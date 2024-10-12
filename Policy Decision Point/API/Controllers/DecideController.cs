using API.Models;
using Excid.Pdp.OpenFGA;
using Excid.Pdp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using OpenFga.Sdk.Client.Model;
using System.Security.Claims;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("api/decide/[action]")]
    [Authorize]
    [RequiredScope("user")]
    public class DecideController : ControllerBase
    {
        private readonly ILogger<DecideController> _logger;
        private readonly AuthorizationPasrer _authorizationPasrer;
        private readonly IOpenFGA _openFGA;

        public DecideController(ILogger<DecideController> logger, IOpenFGA openFGA)
        {
            _logger = logger;
            _authorizationPasrer = new AuthorizationPasrer();
            _openFGA = openFGA;
        }

        [HttpPost]
        public async Task<IActionResult> MakeDecision(DecisionRequest decisionRequest)
        {
            var user = User.Claims.First(q => q.Type == ClaimTypes.NameIdentifier).Value;
            _logger.LogInformation("user is : " + user + " " + JsonSerializer.Serialize(decisionRequest));


           try
           {
               /* 
               UserContext? userContext = await _authorizationPasrer.parse(authorization);
               _logger.LogInformation("user is : " + JsonSerializer.Serialize(userContext));
               if (userContext == null)
               {
                   _logger.LogInformation("Authorization header does not contain a user");
                   return Unauthorized();
               }
               var options = new ClientCheckOptions
               {
                   AuthorizationModelId = _openFGA.Client.AuthorizationModelId,
               };
               */
               var body = new ClientCheckRequest
               {
                   User = "user:" + user,
                   Relation = "can_read",
                   Object = "resource:" + decisionRequest.Resource.Id,

               };
               /* 
               if (userContext.Relationships!= null)
               {
                   body.ContextualTuples = new List<ClientTupleKey>();
                   foreach (var relationship in userContext.Relationships)
                   {
                       body.ContextualTuples.Add(
                           new ClientTupleKey
                           {
                               User = relationship.Subject,
                               Relation = relationship.Relation,
                               Object = relationship.Object
                           }
                       );
                   }
               }*/

               _logger.LogInformation("Sending to OpenFGA:" + JsonSerializer.Serialize(body));
               var response = await _openFGA.Check(body);
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

            */
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
