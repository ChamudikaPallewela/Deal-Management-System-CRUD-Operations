namespace ASE.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Name { get; set; } // Required
        public string Slug { get; set; } // Unique identifier
        public string Video { get; set; } // URL
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
