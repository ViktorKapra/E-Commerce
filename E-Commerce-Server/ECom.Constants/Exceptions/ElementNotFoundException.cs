namespace ECom.Constants.Exceptions
{
    public class ElementNotFoundException : AbstractException
    {
        public ElementNotFoundException(string message = "Element with the given description doesn't exists")
            : base(message)
        { }
        public ElementNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
