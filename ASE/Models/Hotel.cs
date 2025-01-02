namespace ASE.Models
{
    public class Hotel
    {
        public int Id { get; set; } // Matches Hotels.Id
        public string Name { get; set; } // Matches Hotels.Name
        public decimal Rate { get; set; } // Matches Hotels.Rate
        public string Amenities { get; set; } // Matches Hotels.Amenities
        public int? DealId { get; set; } // Matches Hotels.DealId
        public Deal Deal { get; set; } // Navigation property
    }
}
