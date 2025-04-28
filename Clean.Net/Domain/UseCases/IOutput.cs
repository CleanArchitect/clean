namespace Clean.Net;

/// <summary>
/// Defines an Output for a Use Case. <see cref="IUseCase{TInput}"/>
/// </summary>
public interface IOutput { }

/// <summary>
/// Defines a specific Output for when a domain entity is created. 
/// Holds the Id of the created Entity.
/// </summary>
public interface ICreatedOutput : IOutput
{
    Guid? Id { get; }
}

/// <summary>
/// Defines a specific Output for a file.
/// </summary>
public interface IFileOutput : IOutput
{
    byte[] File { get; }
    string Filename { get; }
}

/// <summary>
/// A default empty IOutput, can be used when a use case has no output.
/// </summary>
public static class Output
{
    public static IOutput Empty => new EmptyOutput();

    private sealed class EmptyOutput : IOutput { }
}