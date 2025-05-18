namespace Clean.Net;

/// <summary>
/// Defines an Output for a Use Case. <see cref="IUseCase{TInput}"/>
/// </summary>
public interface IOutput { }

/// <summary>
/// Defines an Output for when a domain entity is created. 
/// Holds the Id of the created Entity.
/// </summary>
public interface ICreatedOutput : IOutput
{
    Guid? Id { get; }
}

/// <summary>
/// Defines an Output for a file.
/// </summary>
public interface IFileExportOutput : IOutput
{
    byte[] File { get; }
    string Filename { get; }
}

public static class Output
{
    /// <summary>
    /// An empty IOutput, can be used when a Use Case has no content for output.
    /// </summary>
    public static IOutput Empty => new EmptyOutput();

    public static ICreatedOutput Created(Guid id) => new CreatedOutput(id);

    public static IFileExportOutput File(byte[] file, string filename) => new FileOutput(file, filename);

    private sealed class EmptyOutput : IOutput { }

    private sealed class CreatedOutput(Guid id) : ICreatedOutput
    {
        public Guid? Id => id;
    }

    private sealed class FileOutput(byte[] file, string filename) : IFileExportOutput
    {
        public byte[] File => file;
        public string Filename => filename;
    }
}