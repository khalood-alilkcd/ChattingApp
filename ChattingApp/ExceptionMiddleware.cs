using ChattingApp.Error_Model;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    /// must do configure the first one midlleware in program
    /// this custom middleware to c atch internal server error to pass statuscode and message and details of exception
    
    /// <summary>
    ///  <param name="logger"></param> this exception logger in console
    ///  <param name="env"></param> environment with development on statuscode, message and detail of exception
    /// </summary>

    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly IHostEnvironment env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        this.next=next;
        this.logger=logger;
        this.env=env;
    }

    public async Task InvokeAsync(HttpContext context) /// context of request accured
    {
        try
        {
            await next.Invoke(context); /// move to next middleware
        }
        catch (Exception ex) // ex is take header object by creating the cli 
        {
            logger.LogError(ex, ex.Message);
            /// log exception at database
            /// response for user 
            /// response contain header and body , the header concern contentType and statusCode
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionErrorResponse = env.IsDevelopment() ?
                new ApiExciptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExciptionResponse(500);
            /// must have camalcase of words of response 
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            /// then must serlizing the object exceptionErrorResponse
            var json = JsonSerializer.Serialize(exceptionErrorResponse, options);

            /// finaly handling body for response
            /// and it take type json
            await context.Response.WriteAsync(json);
        }
    }
    
}