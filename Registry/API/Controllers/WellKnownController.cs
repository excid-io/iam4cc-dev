using Microsoft.AspNetCore.Mvc;
using Excid.Oidc.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace idp.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route(".well-known/[action]")]
    public class WellKnownController : Controller
    {
        private readonly ILogger<WellKnownController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _iss = string.Empty;

        public WellKnownController(IConfiguration configuration, ILogger<WellKnownController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _iss = _configuration.GetValue<string>("Issuer:iss") ?? "";
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Jwks()
        {
            JsonWebKey _publicJWK = new JsonWebKey();
            var JwkSet = new JwkSet();
            string privateKeyPem = _configuration.GetValue<string>("Issuer:PrivateKeyPem") ?? "";
            string privateKeyPemPassord = _configuration.GetValue<string>("Issuer:PrivateKeyPemPassord") ?? "";
            try
            {
                string pemKey = System.IO.File.ReadAllText(privateKeyPem);
                var signingecdsa = ECDsa.Create();
                signingecdsa.ImportFromEncryptedPem(new System.ReadOnlySpan<char>(pemKey.ToCharArray()), privateKeyPemPassord);
                _publicJWK = JsonWebKeyConverter.ConvertFromECDsaSecurityKey(new ECDsaSecurityKey(signingecdsa));
                _publicJWK.D = null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in WellKnownController:" + ex.ToString());
            }
            JwkSet.Keys.Add(_publicJWK);
            return Content(JsonSerializer.Serialize(JwkSet), "application/json");
        }
    }
}
