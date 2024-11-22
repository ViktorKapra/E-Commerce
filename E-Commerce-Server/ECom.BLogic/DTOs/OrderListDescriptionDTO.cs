using System.Security.Claims;

namespace ECom.BLogic.DTOs
{
    public class OrderListDescriptionDTO
    {
        public int OrderListId { get; set; }
        public ClaimsPrincipal UserClaim { get; set; }
    }
}
