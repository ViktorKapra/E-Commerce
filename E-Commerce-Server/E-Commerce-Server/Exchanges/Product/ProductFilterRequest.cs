using ECom.API.Exchanges.Templates;

namespace ECom.API.Exchanges.Product
{
    public class ProductFilterRequest : FilterRequest
    {
        /// <summary>
        /// The age rating of the game. It can be PEGI_3, PEGI_7, PEGI_12, PEGI_18.
        /// </summary>
        public string? AgeRating { get; set; }

        /// <summary>
        /// The genre of the game.
        /// </summary>
        public string? Genre { get; set; }

        /// <summary>
        /// The order type of sorting. It can be ASC or DESC
        /// </summary>
        /// <example>Asc</example>
        public string? OrderType { get; set; }

        /// <summary>
        /// The order property name of sorting. It can be Price and TotalRating.
        /// </summary>
        /// <example>Price</example>
        public string? OrderPropertyName { get; set; }

    }
}
