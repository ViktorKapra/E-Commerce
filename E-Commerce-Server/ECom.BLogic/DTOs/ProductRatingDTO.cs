using System.Security.Claims;

namespace ECom.BLogic.DTOs
{
    public class ProductRatingDTO
    {
        public ClaimsPrincipal UserClaim { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
    }
}
