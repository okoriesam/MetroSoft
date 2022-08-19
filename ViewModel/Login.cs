using System.ComponentModel.DataAnnotations;

namespace Metrosoft.ViewModel
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
