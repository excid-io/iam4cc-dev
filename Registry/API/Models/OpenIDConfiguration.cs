using System.Text.Json.Serialization;

namespace Excid.Oidc.Models
{
    public class OpenIDConfiguration
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = string.Empty;

        [JsonPropertyName("authorization_endpoint")]
        public string AuthorizationEndpoint { get; set; } = string.Empty;

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; } = string.Empty;

		[JsonPropertyName("jwks_uri")]
		public string JwksUri { get; set; } = string.Empty;
	}
}
