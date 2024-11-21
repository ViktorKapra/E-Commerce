namespace ECom.Constants.Exceptions
{
    public class InvalidDeletionException : AbstractException
    {
        public InvalidDeletionException(string message = "Element cannot be deleted.")
            : base(message)
        { }
        public InvalidDeletionException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
