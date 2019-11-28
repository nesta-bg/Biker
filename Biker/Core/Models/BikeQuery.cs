namespace Biker.Core.Models
{
    public class BikeQuery
    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public string SortBy { get; set; }

        public bool IsSortAscending { get; set; }
    }
}
