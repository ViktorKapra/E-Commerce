namespace ECom.Constants.Exceptions
{
    public class InvalidCreationException : AbstractException
    {
        public InvalidCreationException(string message = "Element with the provided data can't be created.")
    : base(message)
        { }
        public InvalidCreationException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
