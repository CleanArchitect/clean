using Microsoft.AspNetCore.Mvc;

namespace Clean.Core;

public abstract class CleanController : ControllerBase
{
    [NonAction]
    protected CreatedAtActionResult CreatedAtAction(string actionName, IOutput output, params (string Key, Func<IOutput, object> Value)[] routeValues)
    {
        var routeValuesDictionary = routeValues
            .ToDictionary(
                pair => pair.Key,
                pair => pair.Value(output));

        return CreatedAtAction(actionName, routeValuesDictionary, output);
    }

    [NonAction]
    protected NoContentResult NoContent(IOutput _) =>
        NoContent();

    [NonAction]
    protected FileResult File(IFileOutput output) =>
        File(output.File, output.Filename.MimeType(), output.Filename);
}
