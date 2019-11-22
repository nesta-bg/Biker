using System.ComponentModel.DataAnnotations.Schema;

namespace Biker.Core.Models
{
    [Table("BikeFeatures")]
    public class BikeFeature
    {
        public int BikeId { get; set; }

        public int FeatureId { get; set; }

        public Bike Bike { get; set; }

        public Feature Feature { get; set; }
    }
}
