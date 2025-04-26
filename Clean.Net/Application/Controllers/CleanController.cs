using Microsoft.AspNetCore.Mvc;

namespace Clean.Net;

public abstract class CleanController : ControllerBase
{
    [NonAction]
    protected CreatedAtActionResult CreatedAtAction(string actionName, ICreatedOutput output, params (string Key, object Value)[] additionalRouteValues)
    {
        var routeValues = additionalRouteValues
            .ToDictionary(
                pair => pair.Key,
                pair => pair.Value);

        routeValues.Add(nameof(output.Id), output.Id);

        return CreatedAtAction(actionName, routeValues, output);
    }

    [NonAction]
    protected NoContentResult NoContent(IOutput _) =>
        NoContent();

    [NonAction]
    protected FileResult File(IFileOutput output) =>
        File(output.File, output.Filename.MimeType(), output.Filename);
}
