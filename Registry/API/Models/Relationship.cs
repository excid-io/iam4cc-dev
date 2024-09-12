using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Excid.Registry.API.Models
{
    public class Relationship
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
        //public int RelationshipObjectID { get; set; }
        //public int RelationshipTypeID { get; set; }
        //public virtual Entity RelationshipObject { get; set; } = new Entity();
        //public virtual Relation RelationshipType { get; set; } = new Relation();
    }
}
