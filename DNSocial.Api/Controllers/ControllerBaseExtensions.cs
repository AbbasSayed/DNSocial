using DNSocial.Api.Contracts.Common.Responses;
using DNSocial.Application.Enums;
using DNSocial.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNSocial.Api.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult HandleErrorResponse(this ControllerBase controller, List<Error> errors)
        {
            var apiError = new ErrorResponse();

            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var error = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);

                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Not Found";
                apiError.Timestamp = DateTime.Now;
                apiError.Errors.Add(error!.Message);

                return controller.NotFound(apiError);
            }

            apiError.StatusCode = 400;
            apiError.StatusPhrase = "Bad request";
            apiError.Timestamp = DateTime.Now;
            errors.ForEach(e => apiError.Errors.Add(e.Message));
            return controller.StatusCode(400, apiError);
        }
    }
}
