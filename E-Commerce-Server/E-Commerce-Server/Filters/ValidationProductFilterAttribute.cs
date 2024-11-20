using ECom.API.Exchanges.Product;
using ECom.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECom.API.Filters
{
    public class ValidationProductFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is ProductFilterRequest);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Request is null");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            var request = (ProductFilterRequest)param.Value;

            if (request.Limit < 0 || request.Offset < 0)
            {
                context.Result = new BadRequestObjectResult("Limit and Offset must be greater than 0");
                return;
            }

            var ageRating = new DataEnums.Rating();
            if (request.AgeRating is not null
                && !Enum.TryParse<DataEnums.Rating>(request.AgeRating, out ageRating))
            {
                context.Result = new BadRequestObjectResult("Age rating must be PEGI_3, PEGI_7, PEGI_12 or PEGI_18");
                return;
            }

            if (request.OrderType is not null
                && request.OrderType.ToLower() != ValidationConsts.SORT_ASC
                && request.OrderType.ToLower() != ValidationConsts.SORT_DESC)
            {
                context.Result = new BadRequestObjectResult("Order type must be asc or desc");
                return;
            }

            if (request.OrderPropertyName is not null
                && request.OrderPropertyName != "Price"
                && request.OrderPropertyName != "TotalRating")
            {
                context.Result = new BadRequestObjectResult("Order property name must be Price or TotalRating");
                return;
            }
            var result = await next();
            // execute any code after the action executes
        }
    }
}
