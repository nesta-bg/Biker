using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biker.Core.Models
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
}
