using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Queries.GetOrderList.v1;
using RunDotnetMicroServicesNuget.Common.Models;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ApiControllerBase
{
    [HttpGet]        
    public async Task<IActionResult> GetOrdersByUsername(GetOrderListQuery request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {

            return BadRequest(BaseResponse.Err(message: ex.Message);
        }
        
    }
}
