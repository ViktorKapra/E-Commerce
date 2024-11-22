using ECom.Constants.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace ECom.Configuration.Middleware
{
    public class ExceptionHandler
    {
        public static string LocalizationKey => "LocalizationKey";

        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {

                case ElementNotFoundException e:
                    code = HttpStatusCode.NotFound;
                    break;
                case AbstractException e:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            return (code, JsonConvert.SerializeObject(exception.GetType().Name + " - " + exception.Message));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // log the error
                Log.Error(exception, "error during executing {Context}", context.Request.Path.Value);
                var response = context.Response;
                response.ContentType = "application/json";

                // get the response code and message
                var (status, message) = GetResponse(exception);
                response.StatusCode = (int)status;
                Log.Warning("Error {Status} {Message}", status, message);
                await response.WriteAsync(message);
            }
        }
    }
}
