using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Platform { get; set; }

        public DateOnly DateCreated { get; set; }


        [Range(1.0, 5.0)]
        public decimal TotalRating { get; set; }

        public decimal Price { get; set; }

        public string? Logo { get; set; }

        public string? Background { get; set; }

        public string Genre { get; set; }

        public int Count { get; set; }

        public string Rating { get; set; }
    }
}
