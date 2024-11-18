namespace ECom.Constants.Exceptions
{
    public class ProductUpdateFailedException : AbstractException
    {
        public ProductUpdateFailedException() { }
        public ProductUpdateFailedException(string message = "Product update failed.")
            : base(message) { }
        public ProductUpdateFailedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
