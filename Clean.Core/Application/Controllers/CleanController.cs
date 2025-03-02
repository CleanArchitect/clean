using Microsoft.AspNetCore.Mvc;

namespace Clean.Core;

public abstract class CleanController : ControllerBase
{
    [NonAction]
    protected CreatedAtActionResult CreatedAtAction(string actionName, IOutput output) =>
        CreatedAtAction(actionName, new { id = output.Id }, output);

    [NonAction]
    protected NoContentResult NoContent(IOutput _) =>
        NoContent();

    [NonAction]
    protected FileResult File(IFileOutput output) =>
        File(output.File, output.Filename.MimeType(), output.Filename);
}
