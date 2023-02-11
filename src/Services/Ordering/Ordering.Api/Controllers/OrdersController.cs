using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.Api.Controllers
{
    [ApiController]
    public class OrdersController : ApiControllerBase
    {

        [HttpGet("api/[controller]/GetOrder/{username}")]
        public IActionResult GetOrderByUsername(string username)
        {
            return Ok();
        }
    }
}
