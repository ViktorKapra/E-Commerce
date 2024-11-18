using ECom.Constants;
using ECom.Data.Interfaces;

namespace ECom.Data.Models
{
    public class Product : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataEnums.Platform Platform { get; set; }
        public DateOnly DateCreated { get; set; }
        public decimal TotalRating { get; set; }
        public decimal Price { get; set; }
        public string? Logo { get; set; }
        public string? Background { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        public DataEnums.Rating Rating { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
