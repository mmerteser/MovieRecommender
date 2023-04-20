using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Application.Utilities.Result;

namespace MovieRecommender.WebAPI.Extensions
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                       .Where(x => x.Value.Errors.Any())
                       .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                       .ToArray();

                var errorStr = String.Join(", ", errors.Select(key => key.Key + " = " + String.Join(", ", key.Value)));

                context.Result = new JsonResult(new ErrorResult(errorStr));
                return;
            }

            await next();
        }
    }
}
