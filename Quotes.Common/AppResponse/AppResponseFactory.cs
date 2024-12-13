using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quotes.Common.AppResponse
{
    public class AppResponseFactory
    {
        public static GenericResponse<T> SuccessResponse<T>(T response)
        {
            return new GenericResponse<T>
            {
                StatusCode = HttpStatusCode.OK,
                Status = ApiStatus.Success.ToString(),
                Errors = null,
                Response = response
            };
        }
        public static GenericResponse<string> BadRequest(List<string> errors)
        {
            return new GenericResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Status = ApiStatus.Failure.ToString(),
                Errors = errors,
            };
        }

        public static GenericResponse<string> BadRequest(string error)
        {
            return new GenericResponse<string>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Status = ApiStatus.Failure.ToString(),
                Errors = [error],
            };
        }

        public static GenericResponse<string> InternalError(string error)
        {
            return new GenericResponse<string>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Status = ApiStatus.Failure.ToString(),
                Errors = [error],
            };
        }
    }
}
