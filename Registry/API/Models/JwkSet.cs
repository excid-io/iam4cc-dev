using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace Excid.Oidc.Models
{
	public class JwkSet
	{
		[JsonPropertyName("keys")]
		public List<JsonWebKey> Keys { get; set; } = new List<JsonWebKey> ();
	}
}
