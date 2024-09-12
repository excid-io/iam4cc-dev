using Excid.Pdp.OpenFGA;
using Excid.Pdp.Security;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client.Model;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("api/authorizer/[action]")]
    public class AuthorizerController : ControllerBase
    {
        private readonly ILogger<AuthorizerController> _logger;
        private readonly AuthorizationPasrer _authorizationPasrer;
        private readonly IOpenFGA _openFGA;

        public AuthorizerController(ILogger<AuthorizerController> logger, IOpenFGA openFGA)
        {
            _logger = logger;
            _authorizationPasrer = new AuthorizationPasrer();
            _openFGA = openFGA;
        }

        [HttpPost]
        public async Task<IActionResult> authorize()
        {
            if (!Request.Headers.ContainsKey("Authorization") || !Request.Headers.ContainsKey("objectId")|| !Request.Headers.ContainsKey("method"))
            {
                return Unauthorized();
            }
            string authorization_header = Request.Headers["Authorization"]!;
            string authorization;
            try
            {
                authorization = authorization_header.Split(' ')[1];
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception in parsing authorization header: " + ex.ToString());
                return Unauthorized();
            }
            string objectId = Request.Headers["objectId"]!;
            string method = Request.Headers["method"]!;
            _logger.LogInformation("authorization is: " + authorization);
            _logger.LogInformation("objectId is: " + objectId);
            _logger.LogInformation("method is: " + method);
            try
            {
                User? user = _authorizationPasrer.parse(authorization);
                _logger.LogInformation("user is : " + JsonSerializer.Serialize(user));
                if (user == null)
                {
                    _logger.LogInformation("Authorization header does not contain a user");
                    return Unauthorized();
                }
                var options = new ClientCheckOptions
                {
                    AuthorizationModelId = _openFGA.Client.AuthorizationModelId,
                };
                var body = new ClientCheckRequest
                {
                    User = "user:" + user.Username,
                    Relation = "can_read",
                    Object = "resource:" + objectId,

                };
                var response = await _openFGA.Client.Check(body, options);
                _logger.LogInformation("Received response from OpenFGA:" + JsonSerializer.Serialize(response));
                if (response!=null && response.Allowed == true)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }

            } catch (Exception ex)
            {
                _logger.LogInformation("Exception in authorization process: " + ex.ToString());
                return Unauthorized();
            }
           

            /*
            string headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + "=" + Request.Headers[key] + Environment.NewLine;
            _logger.LogInformation("headers: " +  headers);
             string content = await new StreamReader(Request.Body).ReadToEndAsync();
            _logger.LogInformation("content: " + content);
            */
            
        }


  
    }
}
