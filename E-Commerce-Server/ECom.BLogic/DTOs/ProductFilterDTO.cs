using ECom.Constants;

namespace ECom.BLogic.DTOs
{
    public class ProductFilterDTO
    {
        public string? Genre { get; set; }
        public string? AgeRating { get; set; }
        public string? OrderType { get; set; } = DefaultValuesConsts.DEFAULT_ORDER_TYPE;
        public string? OrderPropertyName { get; set; } = DefaultValuesConsts.DEFAULT_ORDER_PROPERTY_NAME;
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
