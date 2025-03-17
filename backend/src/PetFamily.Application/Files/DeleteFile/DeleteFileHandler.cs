using CSharpFunctionalExtensions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Files.DeleteFile;

public class DeleteFileHandler
{
    private readonly IFileProvider _fileProvider;

    public DeleteFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> HandleAsync(
        DeleteFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var deleteResult = await _fileProvider.DeleteFileAsync(request.Path, cancellationToken);

        if (deleteResult.IsFailure)
        {
            return deleteResult.Error;
        }

        return deleteResult.Value;
    }
}
