namespace MovieRecommender.WebAPI.Extensions
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddleware> logger;

        public RequestResponseMiddleware(RequestDelegate next, ILogger<RequestResponseMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var originalBodyStream = httpContext.Response.Body;

            logger.LogInformation($"Request: {httpContext.Request.QueryString}");

            var requestBody = new MemoryStream();

            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);


            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await next.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(httpContext.Response.Body, System.Text.Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(originalBodyStream);

            logger.LogInformation($"Request: {requestText}");
            logger.LogInformation($"Response: {responseText}");

            await httpContext.Response.WriteAsync(responseText.Replace("ERR: ", ""));
        }
    }
}
