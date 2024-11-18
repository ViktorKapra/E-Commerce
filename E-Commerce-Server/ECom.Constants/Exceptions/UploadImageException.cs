namespace ECom.Constants.Exceptions
{
    public class UploadImageException : AbstractException
    {
        public UploadImageException(string message = "Image can't be uploaded.")
            : base(message)
        { }
        public UploadImageException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
