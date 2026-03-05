namespace StoresApi.Middleware;

/// <summary>Middleware responsible for ensuring a specific country code is provided in the request headers.</summary>
/// <remarks>
///     <para>
///         If the request does not include a country code in the header, the middleware appends a default country code to the request headers using the header key
///         "x-test-country-code".
///     </para>
///     <para>This middleware should be added to the middleware pipeline when in the dev environment to guarantee that all requests include a country code.</para>
/// </remarks>
public sealed class CountryCodeMiddleware(RequestDelegate next)
{
    private const string CountryCodeHeaderName = "x-test-country-code";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey(CountryCodeHeaderName))
        {
            context.Request.Headers.Append(CountryCodeHeaderName, value: "UK");
        }

        await next(context);
    }
}
