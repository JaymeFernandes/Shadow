using Amazon.Runtime;
using Amazon.S3;
using Domain.Options;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace Api.Extensions;

public static class S3Setup
{
    public static IServiceCollection AddS3Setup(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<S3Options>(configuration.GetSection("S3"));

        services.AddSingleton<IAmazonS3>(x =>
        {
            var options = x.GetRequiredService<IOptions<S3Options>>();
            
            var config = new AmazonS3Config
            {
                ServiceURL = options.Value.Host,
                ForcePathStyle = true,
            };

            var credential = 
                new BasicAWSCredentials(options.Value.User, options.Value.AccessKey);
        
            return new AmazonS3Client(credential, config);
        });

        services.AddScoped<IAwsS3, AwsS3Services>();
        
        return services;
    }
}