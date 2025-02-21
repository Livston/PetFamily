using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extentions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.Dto;
using PetFamily.Application.Volunteers.UpdateHelpDetails;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        //[HttpGet("{id:guid}")]
        //public async IActionResult GetByIdAsync(
        //    [FromRoute] Guid id,
        //    [FromServices] CreateVolunteerHandler handler,
        //    CancellationToken cancellationToken = default)
        //{
        //    //var result = await handler.HandleAsync;
        //    return Ok();

        //}

        [HttpPost]
        public async Task<ActionResult> CreateAsync(
            [FromServices] CreateVolunteerHandler handler,
            [FromServices] IValidator<CreateVolunteerRequest> validator,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandleAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);

        }

        [HttpPut("{id:guid}main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] UpdateMainInfoHandler handler,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = UpdateMainInfoRequest.UpdateMainInfoRequestFromDto(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandleAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/help-details")]
        public async Task<ActionResult> UpdateHelpDetails(
            [FromRoute] Guid id,
            [FromBody] IEnumerable<HelpRequisiteDTO> dtos,
            [FromServices] UpdateHelpDetailsHandler handler,
            [FromServices] IValidator<UpdateHelpDetailsRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateHelpDetailsRequest(id, dtos);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.HandleAsync(request, cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);

        }

    }
}
