using API.Models;
using Excid.Pdp.OpenFGA;
using Excid.Pdp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using OpenFga.Sdk.Client.Model;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    [ApiController]
    [Route("api/decide/[action]")]
    public class DecideController : ControllerBase
    {
        private readonly ILogger<DecideController> _logger;
        private readonly VPPasrer _vpPasrer;
        private readonly IOpenFGA _openFGA;

        public DecideController(ILogger<DecideController> logger, IOpenFGA openFGA)
        {
            _logger = logger;
            _vpPasrer = new VPPasrer();
            _openFGA = openFGA;
        }

        [HttpPost]
        public async Task<IActionResult> MakeDecision(DecisionRequest decisionRequest)
        {
           try
           {
               StringValues code;
               string vp = "";
               Request.Headers.TryGetValue("Authorization", out code);
               if (code.Count > 0)
               {
                   vp = code!.First()!.Split(" ")[1];
               }else
               {
                   return Unauthorized();
               }

               _logger.LogInformation(vp);
               UserContext? userContext = await _vpPasrer.Parse(vp,_logger);
               _logger.LogInformation("user is : " + JsonSerializer.Serialize(userContext));
               if (userContext == null)
               {
                   _logger.LogInformation("Authorization header does not contain a user");
                   return Unauthorized();
               }
               var body = new ClientCheckRequest
               {
                   User = "user:" + userContext.Username,
                   Relation = "reader",
                   Object = "resource:" + decisionRequest.Resource.Id,

               };
 
               if (userContext.Relationships.Count > 0)
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
               }

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
