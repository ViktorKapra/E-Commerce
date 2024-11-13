using ECom.Constants;

namespace ECom.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataEnums.Platform Platform { get; set; }
        public DateOnly DateCreated { get; set; }
        public decimal TotalRating { get; set; }
        public decimal Price { get; set; }
    }
}
