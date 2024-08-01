using Excid.Registry.API.Models.Requests;
using Excid.Registry.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Excid.Registry.API.Data;


namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/relationshiptypes/[action]")]
    public class RelationshipTypesController : ControllerBase
    {


        private readonly ILogger<RelationshipTypesController> _logger;
        private readonly RegistryDBContext _context;

        public string? CurrentUser
        {
            get
            {
                var subClaim = User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.NameIdentifier);
                if (subClaim == null)
                {
                    return null;
                }
                else
                {
                    return subClaim.Value;
                }
            }
        }

        public RelationshipTypesController(ILogger<RelationshipTypesController> logger, RegistryDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (CurrentUser == null) return Unauthorized();
            var items = _context.RelationshipTypes.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(items);
        }

        [HttpGet]
        public IActionResult GetObject(int id)
        {
            if (CurrentUser == null) return Unauthorized();
            var item = _context.RelationshipTypes.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<RelationshipType>> Create(NewRelationshipTypeRequest request)
        {
            if (CurrentUser == null) return Unauthorized();
            /**
             * Check if the the name is not null and unique
             * TODO
             */


            var item = new RelationshipType();
            item.Description = request.Description;
            item.Owner = CurrentUser;
            item.Name = request.Name;
            _context.RelationshipTypes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetObject), new { id = item.Id }, item);
        }
    }
}
