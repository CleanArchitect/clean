using Clean.Net;
using Example.Domain;

namespace Example.Application;

internal static class Examples
{
    public static RouteGroupBuilder ToExamples(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAll).Produces<GetAllExamplesOutput>();

        group.MapPost("/", Create).Produces<ICreatedOutput>();

        group.MapGet("/{id:guid}", Get).Produces<GetExampleOutput>();

        group.MapPatch("/{id:guid}", Patch).Produces<IOutput>();

        group.MapDelete("/{id:guid}", Delete).Produces<IOutput>();

        return group;
    }

    private static async Task<IResult> Get(IInputHandler handler, Guid id) =>
        Results.Ok(await handler.HandleAsync(new GetExampleInput(id)));

    private static async Task<IResult> GetAll(IInputHandler handler) =>
        Results.Ok(await handler.HandleAsync(new GetAllExamplesInput()));

    private static async Task<IResult> Create(IInputHandler handler, CreateExampleInput input) =>
        CreatedAt("/examples", await handler.HandleAsync(input));

    private static async Task<IResult> Patch(IInputHandler handler, Guid id, UpdateExampleInput input) =>
        Results.Ok(await handler.HandleAsync(input.SetId(id)));

    private static async Task<IResult> Delete(IInputHandler handler, Guid id) =>
        Results.Ok(await handler.HandleAsync(new DeleteExampleInput(id)));

    private static IResult CreatedAt(string getRouteUri, ICreatedOutput output) =>
        Results.Created($"{getRouteUri}/{output.Id}", output);
}
