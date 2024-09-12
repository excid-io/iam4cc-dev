using Excid.Pdp.OpenFGA.Models;
using Microsoft.AspNetCore.Mvc;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Client;
using System.Text.Json;
using Excid.Pdp.OpenFGA;

namespace API.Controllers
{
    [Route("api/config/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IOpenFGA _openFGA;

        public ConfigController(ILogger<ConfigController> logger, IOpenFGA openFGA)
        {
            _logger = logger;
            _openFGA = openFGA;

        }

        [HttpPost]
        public async Task<IActionResult> Stores(NewStoreRequest newStoreRequest)
        {
            try
            {
                //var store = await fgaClient.CreateStore(new ClientCreateStoreRequest() { Name = newStoreRequest.Name }) ;
                var response = await _openFGA.Client.CreateStore(new ClientCreateStoreRequest() { Name = newStoreRequest.Name });
                _openFGA.Client.StoreId = response.Id;
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AuthorizationModels(NewAuthorizationModelRequest newAuthorizationModel)
        {
            _logger.LogInformation(JsonSerializer.Serialize(newAuthorizationModel));

            try
            {
                var response = await _openFGA.Client.WriteAuthorizationModel(newAuthorizationModel.Model);
                _openFGA.Client.AuthorizationModelId = response.AuthorizationModelId;
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Tuples(NewTuplesRequest newTuples)
        {
            _logger.LogInformation(JsonSerializer.Serialize(newTuples));

            var options = new ClientWriteOptions
            {
                AuthorizationModelId = _openFGA.Client.AuthorizationModelId
            };
            var body = new ClientWriteRequest()
            {
                Writes = newTuples.Tuples
            };
           
            try
            {
                var response = await _openFGA.Client.Write(body, options);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
