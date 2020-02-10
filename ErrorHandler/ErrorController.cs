using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NetApi.ErrorHandler
{
    [Route("api/error")]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult<ErrorResponse> ErrorHandler()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorResponse()
                    {
                        success = 0,
                        errorno = 999,
                        message = ex.Error.Message
                    });
            }
            else
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorResponse()
                    {
                        success = 0,
                        errorno = 999,
                        message = "ERROR OCCURRED!"
                    });
            }

        }

    }
}