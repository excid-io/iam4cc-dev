using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorizer/[action]")]
    public class AuthorizerController : ControllerBase
    {
        private readonly ILogger<AuthorizerController> _logger;

        public AuthorizerController(ILogger<AuthorizerController> logger)
        {
            _logger = logger;
            
        }

        [HttpPost]
        public IActionResult authorize()
        {
            string headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + "=" + Request.Headers[key] + Environment.NewLine;
            _logger.LogInformation("headers: " +  headers);
            return Ok();
        }
    }
}
