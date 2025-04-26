namespace Clean.Net;

public interface IOutput { }

public interface ICreatedOutput : IOutput
{
    Guid? Id { get; }
}

public interface IFileOutput : IOutput
{
    byte[] File { get; }
    string Filename { get; }
}

public static class Output
{
    public static IOutput Empty => new EmptyOutput();

    private sealed class EmptyOutput : IOutput { }
}