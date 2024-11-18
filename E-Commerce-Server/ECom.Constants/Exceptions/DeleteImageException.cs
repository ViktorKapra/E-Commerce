namespace ECom.Constants.Exceptions
{
    public class DeleteImageException : AbstractException
    {
        public DeleteImageException(string message = "Image can't be deleted.")
            : base(message)
        { }
        public DeleteImageException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
