using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> UploadFileAsync(
            Stream stream,
            string path,
            CancellationToken cancellationToken = default);

        Task<Result<string, Error>> DeleteFileAsync(
            string path,
            CancellationToken cancellationToken = default);
        
        Task<Result<string, Error>> GetFileAsync(
            string path,
            CancellationToken cancellationToken = default);
    }
}
