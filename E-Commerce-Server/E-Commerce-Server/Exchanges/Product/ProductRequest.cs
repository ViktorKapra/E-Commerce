using ECom.Constants;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Product
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// The platform on which the game is available.
        ///</summary>
        ///<remarks>Available platforms are Console, PC, Mobile, VR, Web</remarks>
        ///<example>PC</example>
        [Required]
        public string Platform { get; set; }
        /// <summary>
        /// The date the game was created.
        /// </summary>
        /// <example>2024-01-01</example>
        [Required]
        public DateOnly DateCreated { get; set; }

        [Required]
        [Range(1.0, 5.0)]
        public decimal TotalRating { get; set; }
        /// <summary>
        /// Price of the game.
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        public IFormFile? Logo { get; set; }
        public IFormFile? Background { get; set; }
        [Required]
        //<summary>
        /// The genre of the game.
        /// </summary>
        public string Genre { get; set; }
        [Required]
        public int Count { get; set; } = DefaultValuesConsts.DEFAULT_PRODUCT_COUNT;
        /// <summary>
        /// The age rating.
        ///</summary>
        ///<remarks>Possible ratings are: PEGI_3, PEGI_7, PEGI_12, PEGI_18</remarks>
        ///<example>PEGI_3</example>
        [Required]
        public string Rating { get; set; }
    }
}
