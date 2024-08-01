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
            if (!Request.Headers.ContainsKey("Authorization") || !Request.Headers.ContainsKey("X-ID"))
            {
                return Unauthorized();
            }
            string authorization = Request.Headers["Authorization"]!;
            string objectId = Request.Headers["X-ID"]!;
            _logger.LogInformation("authorization is: " + authorization);
            _logger.LogInformation("objectId is: " + objectId);

            /*
            string headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + "=" + Request.Headers[key] + Environment.NewLine;
            _logger.LogInformation("headers: " +  headers);
             string content = await new StreamReader(Request.Body).ReadToEndAsync();
            _logger.LogInformation("content: " + content);
            */
            return Ok();
        }


  
    }
}
