using System.ComponentModel.DataAnnotations;

namespace Excid.Registry.API.Models
{
    public class RelationshipObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
