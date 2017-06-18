using System.ComponentModel.DataAnnotations;

namespace ImageSeeds.Codes.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid format for email address")]
        public string Email { get; set; }
    }
}