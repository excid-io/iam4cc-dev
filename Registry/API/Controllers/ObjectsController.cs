using Excid.Registry.API.Data;
using Excid.Registry.API.Models;
using Excid.Registry.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/objects/[action]")]
    public class ObjectsController : ControllerBase
    {
        private readonly ILogger<ObjectsController> _logger;
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

        public ObjectsController(ILogger<ObjectsController> logger, RegistryDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (CurrentUser == null) return Unauthorized();
            var items = _context.RelationshipObjects.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(items);
        }

        [HttpGet]
        public IActionResult GetObject(int id)
        {
            if (CurrentUser == null) return Unauthorized();
            var item = _context.RelationshipObjects.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<RelationshipObject>> Create(NewRelationshipObjectRequest request)
        {
            if (CurrentUser == null) return Unauthorized();
            /**
             * Check if the the name is not null and unique
             * TODO
             */
            

            var item = new RelationshipObject();
            item.Description = request.Description;
            item.Owner = CurrentUser;
            item.Name = request.Name;
            _context.RelationshipObjects.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetObject), new { id = item.Id }, item);
        }
    }
}
