using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Queries.GetOrderList.v1;
using Ordering.Application.Orders.Commands.CheckoutOrder.v1;
using Ordering.Application.Orders.Commands.UpdateOrder.v1;
using Ordering.Application.Orders.Commands.DeleteOrder.v1;
using RunDotnetMicroServicesNuget.Common.Models;

namespace Ordering.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ApiControllerBase
{
    public OrdersController()
    {
        
    }

    [HttpGet(Name ="GetOrders")]        
    public async Task<IActionResult> GetOrdersByUsername(GetOrderListQuery request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }        
    }

    [HttpPost(Name ="CheckoutOrder")]
    public async Task<IActionResult> CheckoutOrder(CheckoutOrderCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));
        }
    }

    [HttpPost(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(UpdateOrderCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result));
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message:ex.Message));
        }
    }

    [HttpDelete(Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(DeleteOrderCommand request)
    {
        try
        {
            var result = await Mediator.Send(request);
            return Ok(BaseResponse.Ok(result)); 
        }
        catch (Exception ex)
        {
            return BadRequest(BaseResponse.Err(message: ex.Message));            
        }
    }

}
