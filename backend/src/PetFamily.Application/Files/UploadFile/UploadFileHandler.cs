using CSharpFunctionalExtensions;
using PetFamily.Application.Files.UploadFile;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Files.Upload;


public class UploadFileHandler
{
    private readonly IFileProvider _fileProvider;

    public UploadFileHandler(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<Result<string, Error>> HandleAsync(UploadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var uploadResult = await _fileProvider.UploadFileAsync(request.Stream, request.Path, cancellationToken);

        if (uploadResult.IsFailure)
        {
            return uploadResult.Error;
        }

        return uploadResult.Value;
    }
}
