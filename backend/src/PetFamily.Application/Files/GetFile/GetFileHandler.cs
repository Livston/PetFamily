using CSharpFunctionalExtensions;
using PetFamily.Application.Files.DeleteFile;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Files.GetFile;

public class GetFileHandler
{
    private readonly IFileProvider _fileProvider;

    public GetFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> HandleAsync(
        GetFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _fileProvider.GetFileAsync(request.Path, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        return result.Value;
    }
}
