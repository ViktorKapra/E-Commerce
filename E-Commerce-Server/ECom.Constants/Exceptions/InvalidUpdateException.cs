namespace ECom.Constants.Exceptions
{
    public class InvalidUpdateException : AbstractException
    {
        public InvalidUpdateException(string message = "Invalid update.")
            : base(message) { }
        public InvalidUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
