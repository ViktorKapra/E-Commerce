using ECom.Constants.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace ECom.Configuration.Middleware
{
    public class ExceptionHandler
    {
        /// <summary>
        /// This key should be used to store the exception in the <see cref="IDictionary{TKey,TValue}"/> of the exception data,
        /// to be localized in the abstract handler.
        /// </summary>
        public static string LocalizationKey => "LocalizationKey";

        private readonly RequestDelegate _next;

        /// <summary>
        /// Gets HTTP status code response and message to be returned to the caller.
        /// Use the ".Data" property to set the key of the messages if it's localized.
        /// </summary>
        /// <param name="exception">The actual exception</param>
        /// <returns>Tuple of HTTP status code and a message</returns>
        //public  (HttpStatusCode code, string message) GetResponse(Exception exception);

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public (HttpStatusCode code, string message) GetResponse(Exception exception)
        {
            HttpStatusCode code;
            switch (exception)
            {
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
