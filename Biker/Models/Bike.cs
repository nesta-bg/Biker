using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Biker.Models
{
    public class Bike
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public bool IsRegistered { get; set; }

        public Contact Contact { get; set; }

        public DateTime LastUpdate { get; set; }
    }

    [Owned]
    public class Contact
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
    }
}
