namespace Clean.Core;

public interface IOutput
{
    Guid? Id => throw new NotImplementedException();
}

public static class Output
{
    public static IOutput Empty => new EmptyOutput();

    private sealed class EmptyOutput : IOutput { }
}

