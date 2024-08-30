using Excid.Pdp.OpenFGA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Client;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/config/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private string _apiUrl = string.Empty;
        private string _storeId = string.Empty;

        public ConfigController(ILogger<ConfigController> logger)
        {
            _logger = logger;
            _apiUrl = "http://localhost:8080";

        }

        [HttpPost]
        public async Task<IActionResult> Stores(NewStoreRequest newStoreRequest)
        {

            var configuration = new ClientConfiguration()
            {
                ApiUrl = _apiUrl
                //AuthorizationModelId = Environment.GetEnvironmentVariable("FGA_MODEL_ID"), // optional, can be overridden per request
            };
            var fgaClient = new OpenFgaClient(configuration);
            try
            {
                var store = await fgaClient.CreateStore(new ClientCreateStoreRequest() { Name = newStoreRequest.Name });
                _storeId = store.Id;
                return new JsonResult(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationModels(NewAuthorizationModel newAuthorizationModel)
        {
            _logger.LogInformation(JsonSerializer.Serialize(newAuthorizationModel));

            var configuration = new ClientConfiguration()
            {
                ApiUrl = _apiUrl,
                StoreId = newAuthorizationModel.StoreId
                //AuthorizationModelId = Environment.GetEnvironmentVariable("FGA_MODEL_ID"), // optional, can be overridden per request
            };
            var fgaClient = new OpenFgaClient(configuration);
            try
            {
                var response = await fgaClient.WriteAuthorizationModel(newAuthorizationModel.Model);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
