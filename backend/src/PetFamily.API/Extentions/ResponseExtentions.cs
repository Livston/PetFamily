using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extentions
{
    public static class ResponseExtentions
    {
        public static ActionResult ToResponse(this Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            var responseError = new ResponseError(error.Code, error.Message, null);

            var envelope = Envelope.Error([responseError]);

            return new ObjectResult(envelope)
            {
                StatusCode = statusCode,
            };

        }

        public static ActionResult ToValidationErrorResponse(this ValidationResult result)
        {
            if (result.IsValid)
                throw new InvalidOperationException("result can not be succeed");


            var validationErrors = result.Errors;

            List<ResponseError> responseErrors = [];

            foreach (var validationError in validationErrors)
            {
                var errorMessage = validationError.ErrorMessage;

                var error = Error.Deserialize(errorMessage);

                var reponseError = new ResponseError(
                    error.Code,
                    error.Message,
                    validationError.PropertyName);

                responseErrors.Add(reponseError);

            }

            var envelope = Envelope.Error(responseErrors);

            return new ObjectResult(envelope)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

        }
    }
}
