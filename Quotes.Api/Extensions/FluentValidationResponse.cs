using Microsoft.AspNetCore.Mvc;
using Quotes.Common.AppResponse;

namespace Quotes.Api.Extensions
{
    public static class FluentValidationResponse
    {
        public static IMvcBuilder ConfigureFluentValidationResponse(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelState = actionContext.ModelState;
                    var errors = modelState.Where(x => x.Value.Errors.Any())
                                 .SelectMany(x => x.Value.Errors, (y, z) => new { Field = y.Key, z.ErrorMessage })
                                 .GroupBy(x => x.Field)
                                 .Select(group => $"{group.Key}: {string.Join(", ", group.Select(error => error.ErrorMessage))}")
                                 .ToList();
                    return new BadRequestObjectResult(AppResponseFactory.BadRequest(errors));
                };
            });

            return builder;
        }
    }
}
