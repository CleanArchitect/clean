using Clean.Core;
using Example.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Example.Application;

[ApiController]
[Route("[controller]")]
public sealed class ExamplesController(IInputHandler handler) : CleanController
{
    [HttpGet]
    public async Task<ActionResult<GetAllExamplesOutput>> GetAll() =>
        Ok(await handler.HandleAsync(new GetAllExampleInput()));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) =>
        Ok(await handler.HandleAsync(new GetExampleInput(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExampleInput input) =>
        CreatedAtAction(nameof(Get), await handler.HandleAsync(input));

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExampleInput input) =>
        Ok(await handler.HandleAsync(input.SetId(id)));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) =>
        NoContent(await handler.HandleAsync(new DeleteExampleInput(id)));
}
