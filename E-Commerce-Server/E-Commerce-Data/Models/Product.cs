using ECom.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECom.Data.Models
{
    [Index(nameof(Name))]
    [Index(nameof(Platform))]
    [Index(nameof(DateCreated), IsDescending = new bool[] { true })]
    [Index(nameof(TotalRating), IsDescending = new bool[] { true })]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataEnums.Platform Platform { get; set; }
        public DateOnly DateCreated { get; set; }
        [Column(TypeName = "decimal(2,1)")] // 2 digits before the decimal point and 1 digit after
        [Range(1.0, 5.0)]
        public decimal TotalRating { get; set; }
        [Column(TypeName = "decimal(18,2)")] // 18 digits before the decimal point and 2 digits after
        public decimal Price { get; set; }
    }
}
