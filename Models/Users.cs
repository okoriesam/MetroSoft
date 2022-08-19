using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Metrosoft.Models
{
    public class Users : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
