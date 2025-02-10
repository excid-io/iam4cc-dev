using Excid.Pdp.OpenFGA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Excid.Pdp.OpenFGA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;

namespace API.Controllers
{
    [Route("api/admin/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IOpenFGA _openFGA;

        public AdminController(ILogger<AdminController> logger, IOpenFGA openFGA)
        {
            _logger = logger;
            _openFGA = openFGA;

        }
        
        [HttpPost]
        public async Task<IActionResult> Stores(NewStoreRequest newStoreRequest)
        {
            try
            {
                var response = await _openFGA.NewStore(newStoreRequest);
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
                var response = await _openFGA.NewModel(newAuthorizationModel);
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
            try
            {
                var response = await _openFGA.NewTuples(newTuples);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
