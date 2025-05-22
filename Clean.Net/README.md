# Clean.Net

Clean.Net is a lightweight C# .NET package implementing Clean Architecture with Domain-Driven Design principles. It encapsulates interfaces and classes for the Application, Infrastructure, and Domain layers, helping you create a modular, testable, and maintainable code structure.

## Key Features

- **Separation of Concerns:**  
  Based on a three-layer architecture:
  - **Domain:** Define your domain model with entities here, domain events, and Use Cases.
  - **Application:** Contains your application, for example an ASP.NET Web API with controllers and/or end points.
  - **Infrastructure:** Technical support, including Databases or other persistance, external APIs and other services.

- **Main Interfaces and Services:**  
  - **Clean Architecture Interfaces:**  
    - `IInput`
    - `IUseCase<TInput>`
	- `IOutput`
	- `IEntityGateway<TEntity>`
  - **DDD Classes/Interfaces:**
    - `Entity`
    - `IEvent`
    - `IEventHandler<TEvent>`
  - **Core Services:**  
    - `IInputHandler` – to route application inputs to the proper Use Case.
    - `IEventBus` – for domain event dispatching.
	- `IEntityGateway<TEntity>` – if using Entity Framework.

## Setup

To wire everything up, make sure you call the following registration method from your Domain assembly (the assembly containing all your Entities, Use Cases and Events):

```csharp
services.AddCleanNet();
```

This method registers all necessary services for handling Use Cases and Domain Events.

## Architecture Overview

The package encourages a clean flow from the web request to the data persistence. For an ASP.NET Web API, the typical flow is:

```
      HTTP Client 
         │   ▲   
 request │   │ response  
         ▼   │ 
Controller / Endpoint
         │   ▲ 
   input │   │ output
         ▼   │  
    IInputHandler
         │   ▲  
   input │   │ output
         ▼   │
   IUseCase<TInput>          IEventHandler<TEvent>
         │   ▲                      ▲  
  entity │   │ entity               │ event
         ▼   │                      │ 
IEntityGateway<TEntity>  ── ── ► IEventBus
                          event
```

And the dependencies between layers should look like:

```
         +---------------------+
         |    Domain Layer     |
         | (Entities, UseCases,|
         |  Domain Events)     |
         +------▲----------▲---+
               /           │
              /            │
             /             │
 +--------------------+    │
 | Application Layer  |    │
 | (Controllers, etc) |    │
 +--------------------+    │
             \             │
              \            │    
               \           │
         +------▼--------------+
         | Infrastructure Layer|
         | (EF, Mongo, etc)    |
         +---------------------+
```

## Getting Started

### 1. Domain Layer Setup

- **Defining Domain Entities:**  

Your entities must derive from the base `Entity`. For example:
```csharp
internal class Person : Entity // Base class for all Entities in Clean.Net
{
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }

    public Person(CreatePersonInput input)
    {
        FirstName = input.FirstName;
        LastName = input.LastName;
        
        // Optionally, add a domain event on creation:
        events.Add(new PersonCreatedEvent(this));
    }
}
```

- **Use Cases:**  

Use Cases must implement `IUseCase<TInput>`. Two typical use cases might be to create and fetch a Person Entity.
  
First create an `IInput`, for example to create a Person implement the `ICreateInput`:
  
```csharp
public sealed class CreatePersonInput : ICreateInput // Clean.Net has IInput, ICreateInput and IFileExportInput
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}
```

Then you can create your Use Case to create a Person.
  
```csharp
internal sealed class CreatePersonUseCase(IEntityGateway<Person> gateway) : IUseCase<CreatePersonInput> // Use Case interface from Clean.Net
{
    public async Task<IOutput> ExecuteAsync(CreatePersonInput input)
    {
        var person = new Person(input);

        await gateway
            .Add(person)
            .SaveChangesAsync();

        return Output.Created(person.Id); // Clean.Net has Output.Empty, Output.Created(Guid id) and Output.File(byte[] file, string filename)
    }
}
```
  
Use the interface `IOutput` to create your own custom Output for a Use Case.

- **Domain Events:**

Define domain events by implementing `IEvent` and create a handler using `IEventHandler<TEvent>`. 

```csharp
internal sealed class PersonCreatedEvent(Person person) : IEvent // Clean.Net 
{
    public Person Person => person;
}
```

And the event handler:

```csharp
internal sealed class PersonCreatedEventHandler : IEventHandler<PersonCreatedEvent> // Clean.Net 
{
    public async Task HandleAsync(PersonCreatedEvent raisedEvent) =>
        await Task.Run(() => Console.WriteLine($"Person created with id: {raisedEvent.Person.Id}"));
}
```

Events from `Person.Events` will be raised after `IEntityGateway<Person>.SaveChangesAsync()` is called, to manually raise events call:
```csharp
await eventBus.RaiseEventAsync(new PersonCreatedEvent(createdPerson));
```

### 2. Application Layer Setup

- **Web API Controllers:**  
The package provides an abstract `CleanController` to simplify controller responses from the `IInputHandler`.

For example:

```csharp
[ApiController]
[Route("[controller]")]
public sealed class PersonsController(IInputHandler handler) : CleanController // Clean.Net
{
    [HttpPost]
    public async Task<ActionResult<ICreatedOutput>> Create([FromBody] CreatePersonInput input) =>
        CreatedOutputAt(nameof(GetById), await handler.HandleAsync(input)); // Will set the Person Id from the ICreateOutput as Route Value

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetPersonByIdOutput>> GetById([FromRoute] Guid id) =>
        Ok(await handler.HandleAsync(new GetPersonByIdInput(id)));

    // Additional endpoints for update and delete...
}
```

- **Configuration Helpers:**  

Bind configuration from `appsettings.json` to a C# class.

For Example:

```json
{
    "ApplicationName": "MyApplication"
}
```

```csharp
internal sealed class AppSettings : Settings // Clean.Net
{
    [Required]
    public string ApplicationName { get; private set; }
}

var appSettings = builder.Configuration.GetAppSettings<AppSettings>(); // Will throw exception when ApplicationName is empty
```

You can also register and validate (with [Data Annotations](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs)) other `Settings` classes as singletons using:

```csharp
services.AddSettings(myOtherSettings);
```

### 3. Infrastructure Layer Setup
The package provides a default `IEntityGateway<TEntity>` implementation for Entity Framework.

To use this, register the following:

```csharp
services.AddCleanEntityFramework<TDbContext>(
    options => options.UseSqlServer("YourConnectionString"),
    typeEntityGateway: null // Optional type for custom IEntityGateway<TEntity> implementation
);
```

If needed you can inherit from `EntityFrameworkRepository<TEntity>` and override methods with custom functionality. 

### 4. Additional Helpers & Extensions

The package also provides:

- **Application Layer Helpers:**  
For example, a convention to transform route tokens use `KebabCaseOutboundParameterTransformer`:
  
```csharp
services.AddControllers(options => 
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseOutboundParameterTransformer()))
);
```

- **General Extension Methods:**  
Usefull extension methods for example scanning assemblies for service implementations and registering them to the `ServiceCollection`.
```csharp
services.AddServiceImplementations(typeof(IMyInterface)); // default Assembly.GetCallingAssembly() and ServiceLifetime.Scoped
```
  
- **Type Extension:**  
Use the method `Type.Implements(Type interfaceType)` to check if a `Type` implements a given interface type, supports open generic interface types. For example:
```csharp
typeof(EntityFrameworkRepository<>).Implements(typeof(IEntityGateway<>)); // true
```
   
- **String Extensions:**  
And converting strings to various cases (`ToKebabCase()`, `ToSnakeCase()`, `ToPascalCase()`, `ToCamelCase()`). For example:
```csharp
var kebabCase = "myKebabCaseString".ToKebabCase(); // my-camel-case-string
var pascalCase = "my-pascal.case".ToPascalCase(); // MyPascalCase
```

- **MIME types:**   
The package also contains a small helper class for MIME type resolution making file handling simpler. Usage:
  
```csharp
var mimeType = "document.pdf".ToMimeType(); // application/pdf
var rarMimeType = MimeType.Rar; // application/vnd.rar
```

---

## Conclusion

Clean.Net is designed to accelerate the development of robust, well-structured .NET applications by encapsulating the boilerplate of Clean Architecture and DDD. With clear separation between domain, application, and infrastructure concerns, you can focus on your business logic. Whether you’re creating new use cases or extending existing features, Clean.Net provides a strong foundation for building scalable systems.

Happy coding!

---

If you need more examples or further customization, feel free to explore extending the provided interfaces and helpers to best fit your project’s needs or checkout my [Github Repository](https://github.com/CleanArchitect/clean). Enjoy building with Clean.Net!