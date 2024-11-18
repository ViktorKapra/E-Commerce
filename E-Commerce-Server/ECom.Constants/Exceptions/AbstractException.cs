namespace ECom.Constants.Exceptions
{
    public abstract class AbstractException : System.Exception
    {
        public AbstractException() : base()
        { }
        public AbstractException(string message) : base(message)
        { }
        public AbstractException(string message, System.Exception innerException) : base(message, innerException)
        { }
    }
}
