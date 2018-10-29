using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ImSoPunny.Models
{
    public class AppUser : IdentityUser
    {

        public AppUser()
        {
            Puns = new HashSet<Pun>();
        }
        public ICollection<Pun> Puns { get; set; }
    }

    public class CredentialsModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}