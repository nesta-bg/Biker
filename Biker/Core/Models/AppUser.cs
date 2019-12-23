using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Biker.Core.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(150)]
        public string FullName { get; set; }
    }
}
