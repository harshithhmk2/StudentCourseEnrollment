using System.Net;
using System.Text.Json;
using StudentManagement.Exceptions;

namespace StudentManagement.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred");

                context.Response.ContentType = "application/json";

                var response = context.Response;

                object result;

                switch (ex)
                {
                    case StudentNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        result = new { success = false, message = ex.Message };
                        break;

                    case ArgumentException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = new { success = false, message = ex.Message };
                        break;
                    case UserAlreadyExistsException:
                        response.StatusCode= (int)HttpStatusCode.BadRequest;
                        result =new {success=false,message=ex.Message};
                        break;
                    case DuplicateEnrollmentException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result = new { success = false, message = ex.Message };
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result = new { success = false, message = "Internal Server Error" };
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}