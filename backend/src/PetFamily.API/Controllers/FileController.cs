using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extentions;
using PetFamily.Application.Files.DeleteFile;
using PetFamily.Application.Files.GetFile;
using PetFamily.Application.Files.Upload;
using PetFamily.Application.Files.UploadFile;

namespace PetFamily.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(
            IFormFile file,
            [FromServices] UploadFileHandler handler,
            CancellationToken cancellationToken)
        {
            var path = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            await using var stream = file.OpenReadStream();

            var request = new UploadFileRequest(stream, path);

            var uploadResult = await handler.HandleAsync(request, cancellationToken);

            if (uploadResult.IsFailure)
            {
                return uploadResult.Error.ToResponse();
            }

            return Ok(uploadResult.Value);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletFile(
            [FromQuery] string path,
            [FromServices] DeleteFileHandler handler,
            CancellationToken cancellationToken)
        {
            var request = new DeleteFileRequest(path);

            var deleteResult = await handler.HandleAsync(request, cancellationToken);

            if (deleteResult.IsFailure)
            {
                return deleteResult.Error.ToResponse();
            }

            return Ok(deleteResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(
            [FromQuery] string path,
            [FromServices] GetFileHandler handler,
            CancellationToken cancellationToken)
        {
            var request = new GetFileRequest(path);

            var result = await handler.HandleAsync(request, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }


    }
}
