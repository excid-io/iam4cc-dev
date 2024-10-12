using Microsoft.AspNetCore.Mvc;

namespace API.Models
{
    public class DecisionRequest
    {
        [BindProperty(Name = "resource")]
        public required Resource Resource { get; set; }

        [BindProperty(Name = "action")]
        public required Action Action { get; set; }
    }

    public class Resource
    {
        [BindProperty(Name = "type")]
        public required string Type { get; set; }

        [BindProperty(Name = "id")]
        public required string Id { get; set; }
    }

    public class Action
    {
        [BindProperty(Name = "name")]
        public required string Name { get; set; }
    }
}
