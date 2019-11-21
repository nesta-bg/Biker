using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Biker.Controllers.Resources
{
    public class BikeResource
    {

        public int Id { get; set; }

        public ModelResource Model { get; set; }

        public MakeResource Make { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<FeatureResource> Features { get; set; }

        public BikeResource()
        {
            Features = new Collection<FeatureResource>();
        }

    }
}
