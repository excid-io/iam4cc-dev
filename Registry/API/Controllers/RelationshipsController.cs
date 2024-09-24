using Excid.Registry.API.Data;
using Excid.Registry.API.Models.Requests;
using Excid.Registry.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Relationships/[action]")]
    public class RelationshipsController : ControllerBase
    {
        private readonly ILogger<RelationshipsController> _logger;
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
        public RelationshipsController(ILogger<RelationshipsController> logger, RegistryDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (CurrentUser == null) return Unauthorized();
            var items = _context.Relationships.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(items);
        }

        [HttpGet]
        public IActionResult GetObject(int id)
        {
            if (CurrentUser == null) return Unauthorized();
            var item = _context.Relationships.Where(q => q.Owner == CurrentUser).ToList();
            return new JsonResult(item);
        }

        [HttpPost]
        public async Task<ActionResult<Relationship>> Create(NewRelationshipRequest request)
        {
            if (CurrentUser == null) return Unauthorized();
            /**
             * Check if the the name is not null and unique
             * TODO
             */

            var item = new Relationship();
            item.Owner = CurrentUser;
            item.Subject = request.User;
            item.Relation = request.Relation;
            item.Object = request.Object;
            item.Description= request.Description;
            _context.Relationships.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetObject), new { id = item.Id }, item);
        }
    }
}
