using System.ComponentModel.DataAnnotations;

namespace ECom.BLogic.Services.DTOs
{
    public class ProductSearchDTO
    {
        public string? Name { get; set; }

        public string? Platform { get; set; }

        public DateOnly? DateCreated { get; set; }
        [Range(1.0, 5.0)]
        public decimal? TotalRating { get; set; }

        public decimal? Price { get; set; }
        public string? Genre { get; set; }
        public string? Rating { get; set; }

        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
