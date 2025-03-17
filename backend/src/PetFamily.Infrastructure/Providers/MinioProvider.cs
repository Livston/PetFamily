using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using Minio.Exceptions;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private readonly MinioOptions _minioOptions;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger, IOptions<MinioOptions> minioOptions)
    {
        _minioClient = minioClient;
        _logger = logger;
        _minioOptions = minioOptions.Value;
    }

    public async Task<Result<string, Error>> UploadFileAsync(
        Stream stream,
        string path,
        CancellationToken cancellationToken = default)
    {
        var bucketName = _minioOptions.Bucket;
        if (String.IsNullOrWhiteSpace(bucketName)) 
        {
            throw new ApplicationException("minio bucketName settings");
        }

        try
        {
            var bucketExistArg = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArg, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArg = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArg, cancellationToken);
            }

            var putObjectFile = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithObject(path);

            var result = await _minioClient.PutObjectAsync(putObjectFile, cancellationToken);

            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "minio error");
            return Error.Failure("minio.upload", "fail to upload");
        }
    }

    public async Task<Result<string, Error>> DeleteFileAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        var bucketName = _minioOptions.Bucket;
        if (String.IsNullOrWhiteSpace(bucketName))
        {
            throw new ApplicationException("minio bucketName settings");
        }

        try
        {
            RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(path);

            await _minioClient.RemoveObjectAsync(rmArgs, cancellationToken);
            return path;

        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "minio error");
            return Error.Failure("minio.delete", "fail to delete");
        }
    }

    public async Task<Result<string, Error>> GetFileAsync(string path, CancellationToken cancellationToken = default)
    {
        var bucketName = _minioOptions.Bucket;
        if (String.IsNullOrWhiteSpace(bucketName))
        {
            throw new ApplicationException("minio bucketName settings");
        }

        try
        {
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                      .WithBucket(bucketName)
                                      .WithObject(path)
                                      .WithExpiry(60);

            return await _minioClient.PresignedGetObjectAsync(args);

        }
        catch (MinioException ex)
        {
            _logger.LogError(ex, "minio error");
            return Error.Failure("minio.get", "fail to get");
        }
    }
}
