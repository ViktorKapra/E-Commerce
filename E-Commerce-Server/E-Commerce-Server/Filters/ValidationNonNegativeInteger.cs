using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECom.API.Filters
{
    public class ValidationNonNegativeInteger : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            var param = context.ActionArguments.SingleOrDefault(p => p.Value is int?);
            if (param.Value is not null && (int)param.Value < 0)
            {
                context.Result = new BadRequestObjectResult("Id must be non-negative");
                return;
            }
            var result = await next();

        }
    }
}
