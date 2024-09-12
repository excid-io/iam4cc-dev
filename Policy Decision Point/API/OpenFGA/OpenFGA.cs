using OpenFga.Sdk.Client;
using static System.Net.WebRequestMethods;

namespace Excid.Pdp.OpenFGA
{
    public class OpenFGA : IOpenFGA
    {
        public required OpenFgaClient Client { get; set; }

        private string _openFGAEndpoint { get; set; } = string.Empty;

        public OpenFGA()
        {
            //_openFGAEndpoint = "http://localhost:8080";
            _openFGAEndpoint = "http://192.168.1.2:6005";
            var configuration = new ClientConfiguration()
            { 
                ApiUrl = _openFGAEndpoint
            };
            Client = new OpenFgaClient(configuration);
        }
    }
}
