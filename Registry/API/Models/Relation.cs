using System.ComponentModel.DataAnnotations;

namespace Excid.Registry.API.Models
{
    public class Relation
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
