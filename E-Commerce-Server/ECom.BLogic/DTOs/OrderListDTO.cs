using System.Security.Claims;

namespace ECom.BLogic.DTOs
{
    public class OrderListDTO
    {
        public int Id { get; set; }
        public bool IsFinalized { get; set; }
        public ClaimsPrincipal UserClaim { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
