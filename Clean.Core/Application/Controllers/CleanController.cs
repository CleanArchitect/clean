using Microsoft.AspNetCore.Mvc;

namespace Clean.Core;

public abstract class CleanController : ControllerBase
{
    [NonAction]
    public CreatedAtActionResult CreatedAtAction(string actionName, IOutput output) =>
        CreatedAtAction(actionName, new { id = output.Id }, output);

    [NonAction]
    public NoContentResult NoContent(IOutput _) =>
        NoContent();
}
