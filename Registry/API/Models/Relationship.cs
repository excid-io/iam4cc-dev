using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Excid.Registry.API.Models
{
    public class Relationship
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; } = string.Empty;
        public int RelationshipObjectID { get; set; }
        public int RelationshipTypeID { get; set; }
        public virtual RelationshipObject RelationshipObject { get; set; } = new RelationshipObject();
        public virtual RelationshipType RelationshipType { get; set; } = new RelationshipType();
    }
}
