using System.ComponentModel.DataAnnotations;

namespace Metrosoft.ViewModel
{
    public class Register
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
