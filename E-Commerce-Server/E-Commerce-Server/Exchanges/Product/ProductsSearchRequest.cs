using ECom.API.Exchanges.Templates;
using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Product
{
    public class ProductsSearchRequest : SearchRequest
    {

        /// <summary>
        /// The substring that must be contained by name of the searched game.
        /// </summary>
        /// <example>PC game 1</example>
        public string? Name { get; set; }
        /// <summary>
        /// The platform on which the game is available.
        ///</summary>
        ///<remarks>Available platforms are Console, PC, Mobile, VR, Web</remarks>
        ///<example>PC</example>

        public string? Platform { get; set; }
        /// <summary>
        /// The date the game was created.
        /// </summary>
        /// <example>2024-01-01</example>
        public DateOnly? DateCreated { get; set; }
        [Range(1.0, 5.0)]
        public decimal? TotalRating { get; set; }
        /// <summary>
        /// Price of the searched game.
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// The genre of the game.
        /// </summary>
        public string? Genre { get; set; }
        /// <summary>
        /// The age rating of the game.
        /// </summary>
        public string? Rating { get; set; }
    }
}
