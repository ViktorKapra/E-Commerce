using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ECom.API.Exchanges.Order
{
    public class OrderListExchange
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be positive")]
        public int Id { get; set; }

        public bool? IsFinalized { get; set; }

        [Required]
        [MemberNotNull]
        public List<OrderExchange> Orders { get; set; }
    }
}
