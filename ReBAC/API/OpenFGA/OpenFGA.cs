using Excid.Pdp.OpenFGA.Models;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Model;


namespace Excid.Pdp.OpenFGA
{
    public class OpenFGA : IOpenFGA
    {
        private OpenFgaClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenFGA> _logger;


        public OpenFGA(ILogger<OpenFGA> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string _apiURl = _configuration.GetValue<string>("OpenFGA:ApiUrl") ?? "";
            string? _storeId = _configuration.GetValue<string>("OpenFGA:StoreId");
            string? _authorizationModelId = _configuration.GetValue<string>("OpenFGA:AuthorizationModelId");
            var openFGAClientConfiguration = new ClientConfiguration()
            {
                ApiUrl = _apiURl,
            };
            if (_storeId != null)
            {
                openFGAClientConfiguration.StoreId = _storeId;
            }
            if (_authorizationModelId != null)
            {
                openFGAClientConfiguration.AuthorizationModelId = _authorizationModelId;
            }
            _client = new OpenFgaClient(openFGAClientConfiguration);

        }

        public async Task<ClientWriteResponse?> NewTuples(NewTuplesRequest newTuples)
        {

            var body = new ClientWriteRequest()
            {
                Writes = newTuples.Tuples
            };
            var response = await _client.Write(body);
            return response;
        }

        public async Task<CreateStoreResponse?> NewStore(NewStoreRequest newStore)
        {
            var response = await _client.CreateStore(new ClientCreateStoreRequest() { Name = newStore.Name });
            if (response != null)
            {
                _client.StoreId = response.Id;
            }
            return response;

        }

        public async Task<WriteAuthorizationModelResponse?> NewModel(NewAuthorizationModelRequest newModel)
        {
            var response = await _client.WriteAuthorizationModel(newModel.Model);
            if (response != null)
            {
                _client.AuthorizationModelId = response.AuthorizationModelId;
            }
            return response;
        }

        public async Task<CheckResponse?> Check(ClientCheckRequest clientCheckRequest)
        {
            var response = await _client.Check(clientCheckRequest);
            return response;
        }

    }
}
