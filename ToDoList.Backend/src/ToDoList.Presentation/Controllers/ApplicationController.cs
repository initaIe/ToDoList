using Microsoft.AspNetCore.Mvc;
using ToDoList.Presentation.Response;

namespace ToDoList.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}