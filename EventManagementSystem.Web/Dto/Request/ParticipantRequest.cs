using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Web.Dto.Request
{
    public class ParticipantRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [RegularExpression(
            @"^\+?[1-9][0-9]{1,14}$",
            ErrorMessage = "Phone number isn't in correct format"
        )]
        public string ContactNumber { get; set; }
    }
}
