namespace Clean.Core;

public interface IOutput
{
    Guid? Id => throw new NotImplementedException();
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