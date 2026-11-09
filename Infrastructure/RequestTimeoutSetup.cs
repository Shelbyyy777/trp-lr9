using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Timeouts;

namespace EventDaysMvc.Infrastructure;

public static class RequestTimeoutSetup
{
    public static IServiceCollection AddLabRequestTimeouts(this IServiceCollection services)
    {
        services.AddRequestTimeouts(options => options.DefaultPolicy = new RequestTimeoutPolicy
        {
            Timeout = TimeSpan.FromSeconds(5),
            TimeoutStatusCode = StatusCodes.Status504GatewayTimeout,
            WriteTimeoutResponse = async context =>
            {
                context.Response.ContentType = "application/json; charset=utf-8";
                var body = JsonSerializer.Serialize(new
                {
                    code = "TIMEOUT",
                    message = "Превышено время выполнения запроса: 5 секунд."
                });
                // RequestAborted уже отменён. Ответ записываем с отдельным токеном.
                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(body), CancellationToken.None);
            }
        });
        return services;
    }
}
