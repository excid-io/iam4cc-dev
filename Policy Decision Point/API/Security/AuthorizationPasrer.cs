using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Excid.Pdp.Security
{
    public class AuthorizationPasrer
    {

        public async Task<UserContext?> parse(string authorization)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var tempToken =  tokenHandler.ReadToken(authorization);
            string issuer = tempToken.Issuer;
            var jwksEndpoint = issuer +"/.well-known/jwks";
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(jwksEndpoint);
            var jsonWebKeySet = new JsonWebKeySet(json);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "http://192.168.1.2:6004",
                ValidAudience = "sigstore",
                IssuerSigningKeys = jsonWebKeySet.GetSigningKeys()
                //IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) => jsonWebKeySet.GetSigningKeys()
            };
            var validationResult = await  tokenHandler.ValidateTokenAsync(authorization, validationParameters);
            if(validationResult.IsValid == false)
            {
                Console.WriteLine("Error in validating token" + validationResult.Exception.ToString());
                return null;
            }
            UserContext? userContext = new UserContext();
            userContext.Username = validationResult.Claims["sub"].ToString();
            
            userContext.Relationships = JsonSerializer.Deserialize<List<Relationships>>(JsonSerializer.Serialize(validationResult.Claims["relationships"]));
            return userContext;
        }
    }

    public class UserContext
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        public List<Relationships>? Relationships { get; set; } 
    }

    public class Relationships
    {
        public string Subject { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
    }
}
