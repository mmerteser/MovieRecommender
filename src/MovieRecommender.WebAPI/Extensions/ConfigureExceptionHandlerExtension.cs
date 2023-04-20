using Microsoft.AspNetCore.Diagnostics;
using MovieRecommender.Application.Constants;
using MovieRecommender.Application.Utilities.Result;
using System.Net.Mime;
using System.Text.Json;

namespace MovieRecommender.WebAPI.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogCritical(contextFeature.Error.Message);
                        string error_message = contextFeature.Error.Message.ToLower().Contains("err:")
                                                                                    ? contextFeature.Error.Message
                                                                                    : Messages.UnexpectedError;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResult(error_message)));
                    }
                });
            });
        }
    }
}
