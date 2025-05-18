using Microsoft.AspNetCore.Mvc;

namespace Clean.Net;

/// <summary>
/// Provides a base controller for handling common API responses 
/// for <see cref="IOutput"/> that comes from <see cref="IInputHandler"/>.
/// </summary>
public abstract partial class CleanController : ControllerBase
{
    [NonAction]
    protected CreatedAtActionResult CreatedOutputAt(string actionName, ICreatedOutput output, params (string Key, object Value)[] additionalRouteValues)
    {
        var routeValues = additionalRouteValues
            .ToDictionary(
                pair => pair.Key,
                pair => pair.Value);

        routeValues.Add(nameof(output.Id), output.Id);

        return CreatedAtAction(actionName, routeValues, output);
    }

    [NonAction]
    protected NoContentResult NoContentOutput(IOutput _) =>
        NoContent();

    [NonAction]
    protected FileResult FileOutput(IFileExportOutput output) =>
        File(output.File, output.Filename.ToMimeType(), output.Filename);
}
