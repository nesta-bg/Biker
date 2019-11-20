using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Biker.Controllers.Resources
{
    public class BikeResource
    {

        public int Id { get; set; }

        public int ModelId { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public ICollection<int> Features { get; set; }

        public BikeResource()
        {
            Features = new Collection<int>();
        }

    }
}
