using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ImageMagick;
using ImageMagick.Formats;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class AwsS3Services : IAwsS3
{
    private readonly IAmazonS3 _s3Client;
    private readonly HashSet<string> _existingBuckets = new();
    
    public AwsS3Services(IAmazonS3 client)
        => _s3Client = client;
    
    public async Task<bool> UploadFileAsync(string bucket, string key, Stream file, string contentType)
    {
        if (!_existingBuckets.Contains(bucket))
            await EnsureBucketExistsAsync(bucket);
        
        try
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            var fileTransfer = new TransferUtility(_s3Client);

            await fileTransfer.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = key,
                BucketName = bucket,
                ContentType = contentType
            });
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UploadImageAsync(string bucket, string key, IFormFile file)
    {
        using var collection = new MagickImageCollection();
        await collection.ReadAsync(file.OpenReadStream());

        using var stream = new MemoryStream();
            
        collection.Coalesce();
        collection.Optimize();

        if (collection.Count > 1)
        {
            var webpSettings = new WebPWriteDefines
            {
                Lossless = true,
                Method = 6
            };
                
            await collection.WriteAsync(stream, webpSettings);
        }
        else
        {
            var image = collection[0];
            image.Format = MagickFormat.WebP;
                
            await image.WriteAsync(stream);
        }

        stream.Position = 0;
            
        return await UploadFileAsync(bucket, key, stream, "image/webp");
    }


    public async Task EnsureBucketExistsAsync(string bucket)
    {
        var response = await _s3Client.ListBucketsAsync();

        bool exists = response.Buckets != null && response.Buckets.Any(b => b.BucketName == bucket);

        if (!exists)
        {
            await _s3Client.PutBucketAsync(bucket);
        }
    
        _existingBuckets.Add(bucket);
    }
    
    

    public async Task<GetObjectResponse?> GetObjectAsync(string bucket, string key)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = bucket,
                Key = key
            };
        
            var response = await _s3Client.GetObjectAsync(request);

            return response;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}