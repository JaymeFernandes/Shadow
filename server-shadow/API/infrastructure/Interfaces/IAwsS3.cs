using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Interfaces;

public interface IAwsS3
{
    Task<bool> UploadFileAsync(string bucket, string key, IFormFile file);

    Task<GetObjectResponse?> GetObjectAsync(string bucket, string key);
}