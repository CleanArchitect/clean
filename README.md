# Clean.Net

Clean.Net is a C# .NET solution implementing Clean Architecture with Domain-Driven Design. The library, Clean.Net, encapsulates interfaces and classes for the Application, Data, and Domain layers, promoting a modular and maintainable code structure.

## Project Structure

### Clean.Net

- **Application:** Contains helpful extensions methods for controllers and input validation.
- **Domain:** Defines interfaces and classes for UseCases (Inputs, Outputs, and Gateways), Events, and Entities.
- **Data:** Provides interfaces and classes for using EntityFramework with an EF Repository.

## Example Solution: Example

The "Example" solution folder showcases the usage of the Clean.Net library. It consists of three projects:

1. **Example.Application (Web API):**
   - References: Clean.Net, Clean.Data, Clean.Domain
   - Illustrates the application layer structure.

2. **Example.Data (Class Library):**
   - References: Clean.Net, Example.Domain
   - Demonstrates how the data layer integrates with domain layer using Entity Framework Core - Code First approach.

3. **Example.Domain (Class Library):**
   - References: Clean.Net
   - Implements domain-specific logic using interfaces and classes defined in Clean.Net.

## Usage
Explore the example projects to understand how Clean.Net can be utilized in a real-world scenario. Customize the structure based on your project requirements.

## Contributing
Contributions are welcome! Feel free to submit issues, suggest improvements, or create pull requests.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgements

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)