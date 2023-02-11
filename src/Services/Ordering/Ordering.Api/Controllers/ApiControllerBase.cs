using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
