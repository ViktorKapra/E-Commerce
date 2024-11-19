namespace ECom.BLogic.Templates
{
    public class FilterQuery<T> : Query<T>
    {
        public string OrderType { get; set; }
        public string OrderPropertyName { get; set; }
    }
}
