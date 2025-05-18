using Clean.Net;
using Example.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Example.Application;

[ApiController]
[Route("[controller]")]
public sealed class ExamplesController(IInputHandler handler) : CleanController
{
    [HttpGet]
    public async Task<ActionResult<GetAllExamplesOutput>> GetAll() =>
        Ok(await handler.HandleAsync(new GetAllExamplesInput()));

    [HttpPost]
    public async Task<ActionResult<ICreatedOutput>> Create([FromBody] CreateExampleInput input) =>
        CreatedOutputAt(nameof(Get), await handler.HandleAsync(input));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetExampleOutput>> Get(Guid id) =>
        Ok(await handler.HandleAsync(new GetExampleInput(id)));

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<IOutput>> Update(Guid id, [FromBody] UpdateExampleInput input) =>
        Ok(await handler.HandleAsync(input.SetId(id)));

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<IOutput>> Delete(Guid id) =>
        NoContentOutput(await handler.HandleAsync(new DeleteExampleInput(id)));
}