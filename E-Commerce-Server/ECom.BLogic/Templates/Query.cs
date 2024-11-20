using System.Linq.Expressions;

namespace ECom.BLogic.Templates
{
    public class Query<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }

    }
}
