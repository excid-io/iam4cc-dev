using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Excid.Pdp.Security
{
    public class AuthorizationPasrer
    {
        public User? parse(string authorization)
        {
            string authorizationJsonString = Encoding.UTF8.GetString(Convert.FromBase64String(authorization));
            User? user = JsonSerializer.Deserialize<User>(authorizationJsonString);
            return user;
        }
    }

    public class User
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        public List<Relationships>? Relationships { get; set; } 
    }

    public class Relationships
    {

    }
}
