using System.Linq.Expressions;

namespace ECom.BLogic.Templates
{
    public class SearchQuery<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }
        public string? SearchedValue { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }

    }
}
