namespace API.Models.Requests
{
    public class NewRelationshipRequest
    {
        public string User { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
    }

}
