using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ECom.API.Exchanges.Order
{
    public class CreateOrderListRequest
    {
        [Required]
        [MemberNotNull]
        public List<OrderExchange> Orders { get; set; }
    }
}
