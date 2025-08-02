using Api.Exeptions;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

namespace Api.Extensions;

public static class ProblemDetailsSetup
{
    public static IServiceCollection AddProblemDetailsSetup(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, ex) =>
            {
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();

                return env.IsDevelopment() || env.IsStaging();
            };

            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            options.MapToStatusCode<UnauthorizedAccessException>(StatusCodes.Status401Unauthorized);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            options.MapStatusCodeIncluedDetails<AuthExption>(StatusCodes.Status400BadRequest);

        }).AddProblemDetailsConventions();


        return services;
    }

    private static void MapStatusCodeIncluedDetails<TException>(this Hellang.Middleware.ProblemDetails.ProblemDetailsOptions options, int status) where TException : Exception
    {
        options.Map<TException>(ex => new StatusCodeProblemDetails(status)
        {
            Detail = ex.Message
        });
    }
}