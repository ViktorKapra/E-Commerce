using ECom.Constants;
using System.Reflection;

namespace ECom.API.DTOs.ProductDTOs
{
    public class ProductSearchTerm
    {
        public string? SearchedPropertyName { get; set; }
        public string OperatorDescription { get; set; }
        public string? SearchedValue { get; set; }

        private bool IsValidOperatorDescription()
        {
            switch (OperatorDescription)
            {
                case "Substring":
                case "GreaterThan":
                case "LessThan":
                case "None":
                    return true;
                default:
                    return false;
            }
        }
        public BLogicEnums.FilterOpeations GetOperator()
        {
            return OperatorDescription switch
            {
                "Substring" => BLogicEnums.FilterOpeations.Substring,
                "GreaterThan" => BLogicEnums.FilterOpeations.GreaterThan,
                "LessThan" => BLogicEnums.FilterOpeations.LessThan,
                _ => BLogicEnums.FilterOpeations.None
            };
        }
        private bool IsValidOperation()
        {
            if (!IsValidOperatorDescription()) return false;
            if (GetOperator() == BLogicEnums.FilterOpeations.Substring
                && (IsValidPropertyName() &&
                typeof(ProductDTO).GetProperty(SearchedPropertyName!)?.PropertyType != typeof(string)))
            { return false; }
            return true;
        }
        private bool IsValidPropertyName() =>
                   !string.IsNullOrEmpty(SearchedPropertyName) ||
                   typeof(ProductDTO).GetProperties().Any(p => p.Name == SearchedPropertyName);
        private bool IsSearchValueParsable()
        {
            var propertyInfo = typeof(ProductDTO).GetProperty(SearchedPropertyName!);
            try
            {
                Type propertyType = propertyInfo.PropertyType;
                MethodInfo parseMethod = propertyType.GetMethod("Parse", new[] { typeof(string) });

                if (propertyType == typeof(string))
                {
                    return true;
                }
                if (parseMethod != null)
                {
                    object parsedValue = parseMethod.Invoke(null, new object[] { SearchedValue });
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public bool IsValid()
          => (IsValidPropertyName() && IsValidOperation())
             && (GetOperator() == BLogicEnums.FilterOpeations.None
                || (!string.IsNullOrEmpty(SearchedValue) && IsSearchValueParsable()));

    }
}
