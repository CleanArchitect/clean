using Clean.Net;
using Example.Domain;

namespace Example.Application;

internal static class Examples
{
    public static RouteGroupBuilder ToExamples(this RouteGroupBuilder group)
    {
        group.MapGet("/", Get);

        group.MapPost("/", Create);

        group.MapPatch("/{id:guid}", Patch);

        group.MapDelete("/{id:guid}", Delete);

        return group;
    }

    private static async Task<IResult> Get(IInputHandler handler, Guid id) =>
        Results.Ok(await handler.HandleAsync(new GetExampleInput(id)));

    private static async Task<IResult> Create(IInputHandler handler, CreateExampleInput input) =>
        CreatedAt("/examples", await handler.HandleAsync(input) as ICreatedOutput);

    private static async Task<IResult> Patch(IInputHandler handler, Guid id, UpdateExampleInput input) =>
        Results.Ok(await handler.HandleAsync(input.SetId(id)));

    private static async Task<IResult> Delete(IInputHandler handler, Guid id) =>
        Results.Ok(await handler.HandleAsync(new DeleteExampleInput(id)));

    private static IResult CreatedAt(string getRouteUri, ICreatedOutput output) =>
        Results.Created($"{getRouteUri}/{output.Id}", output);
}
