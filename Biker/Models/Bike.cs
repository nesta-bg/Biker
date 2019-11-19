using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biker.Models
{
    [Table("Bikes")]
    public class Bike
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public bool IsRegistered { get; set; }

        public Contact Contact { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<BikeFeature> Features { get; set; }

        public Bike()
        {
            Features = new Collection<BikeFeature>();
        }
    }

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
