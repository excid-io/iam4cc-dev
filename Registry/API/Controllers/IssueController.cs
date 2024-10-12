using Excid.Registry.API.Data;
using Excid.Registry.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Issue/[action]")]
    public class IssueController : Controller
    {
        private readonly ILogger<IssueController> _logger;
        private readonly RegistryDBContext _context;
        private readonly IConfiguration _configuration;

        public string? CurrentUser
        {
            get
            {
                var subClaim = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier);
                if (subClaim == null)
                {
                    return null;
                }
                else
                {
                    return subClaim.Value;
                }
            }
        }
        public IssueController(ILogger<IssueController> logger, RegistryDBContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;

        }

        [HttpGet]
        public IActionResult Jwt()
        {
            if (CurrentUser == null) return Unauthorized();
            List<RelationshipInJWT> relationships = new List<RelationshipInJWT>();
            Relationships("user:" + CurrentUser, relationships);
            ECDsa _signingKey = ECDsa.Create();
            string privateKeyPem = _configuration.GetValue<string>("Issuer:PrivateKeyPem") ?? "";
            string privateKeyPemPassord = _configuration.GetValue<string>("Issuer:PrivateKeyPemPassord") ?? "";
            try
            {
                string pemKey = System.IO.File.ReadAllText(privateKeyPem);
                _signingKey.ImportFromEncryptedPem(new ReadOnlySpan<char>(pemKey.ToCharArray()), privateKeyPemPassord);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in IssueController:" + ex.ToString());
            }
            var iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var exp = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();
            var iss = _configuration.GetValue<string>("Issuer:iss");
            var jwtpayload = new JwtPayload()
            {
                { "iat", iat },
                { "exp", exp },
                { "iss", iss ?? ""},
                { "aud", "sigstore" },
                { "sub", CurrentUser },
                {"relationships",JsonDocument.Parse(JsonSerializer.Serialize(relationships)).RootElement}
            };


            var jwtHeader = new JwtHeader(
                new SigningCredentials(
                    key: new ECDsaSecurityKey(_signingKey),
                    algorithm: SecurityAlgorithms.EcdsaSha256)
                ){
                { "kid", "key1" }
            }; 
            var jwtToken = new JwtSecurityToken(jwtHeader, jwtpayload);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return Ok(jwtTokenHandler.WriteToken(jwtToken));
        }

        private void Relationships(string subject, List<RelationshipInJWT> relationships)
        {
            var items = _context.Relationships.Where(q => q.Subject == subject).ToList();
            foreach (var item in items)
            {
                RelationshipInJWT relationshipInJWT = new RelationshipInJWT()
                {
                    Subject = item.Subject,
                    Object  = item.Object,
                    Relation =  item.Relation
                };
                relationships.Add(relationshipInJWT);
                Relationships(item.Object+"#"+item.Relation, relationships);
            }
            return;
        }
    }
}
