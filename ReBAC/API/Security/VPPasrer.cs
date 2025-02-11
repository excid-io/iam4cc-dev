using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using API.Controllers;

namespace Excid.Pdp.Security
{
    public class VPPasrer
    {

        public async Task<UserContext?> Parse(string verifiablePresentation, ILogger<DecideController> _logger)
        {
            string vp = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(verifiablePresentation));
            _logger.LogInformation("Received:" + vp);
            var pasrsedPresentation = JsonSerializer.Deserialize<VerifiablePresentation>(vp);
            _logger.LogInformation("VP:" + JsonSerializer.Serialize(pasrsedPresentation));
            UserContext? userContext = new UserContext();
            if (pasrsedPresentation != null && pasrsedPresentation.verifiableCredential.Count > 0)
            {
                var credentialSubject = pasrsedPresentation.verifiableCredential[0].credentialSubject;
                userContext.Username = credentialSubject.holderName;
                if (credentialSubject.fluidosRole == "phd_student")
                {
                    userContext.Relationships.Add(new Relationships()
                    {
                        Subject = "user:" + credentialSubject.holderName,
                        Relation = "phd_student",
                        Object = "university:TUA"
                    });
                }
                if (credentialSubject.orgIdentifier == "cs_department")
                {
                    userContext.Relationships.Add(new Relationships(){
                        Subject = "user:" + credentialSubject.holderName,
                        Relation = "cs_department",
                        Object = "university:TUA"
                    });
                }
            }
            return userContext;
        }
    }

    public class UserContext
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        public List<Relationships> Relationships { get; set; } = new List<Relationships>();
    }

    public class Relationships
    {
        public string Subject { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
    }

    public class VerifiablePresentation
    {
        public List<string> type { get; set; } = new List<string>();
        public List<VerifiableCredential> verifiableCredential { get; set; } = new List<VerifiableCredential>();
    }
    public class VerifiableCredential
    {
        public CredentialSubject credentialSubject { get; set; } = new CredentialSubject();
    }
    public class CredentialSubject
    {
        public string fluidosRole { get; set; } = string.Empty;
        public string holderName { get; set; } = string.Empty;
        public string orgIdentifier { get; set; } = string.Empty;
    }
}
