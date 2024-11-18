namespace ECom.Constants.Exceptions
{
    public class NonExistnantException : AbstractException
    {
        public NonExistnantException(string message = "Element with the given description doesn't exists")
            : base(message)
        { }
        public NonExistnantException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
