using System.ComponentModel.DataAnnotations;

namespace HomeAssignment.API.Models
{
    public class ContactVM
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
