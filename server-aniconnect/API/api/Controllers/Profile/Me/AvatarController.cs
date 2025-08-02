using System.Security.Cryptography;
using System.Text;
using Api.Controllers.Profile.Me.Shared;
using Application.DTOs;
using Application.DTOs.Profile;
using Domain.Models.User;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Interfaces;
using Infrastructure.Services.Save.Mongo.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SixLabors.ImageSharp;

namespace Api.Controllers.Profile.Me;

[Tags("Profile")]
public class AvatarController : ProfileControllerBase
{
    private readonly MongoUserContext _context;
    private readonly IAwsS3 _awsS3;

    public AvatarController(IMemoryCache cache, MongoUserContext context, UserHandler userHandler, IAwsS3 awsS3) : base(cache, context, userHandler)
    {
        _context = context;
        _awsS3 = awsS3;
    }

    [HttpPut("avatar")]
    public async Task<IActionResult> ChangeAvatar(IFormFile formFile)
    {
        var user = await GetUserAsync();

        if (user == null)
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User Not Found",
                Detail = "The user associated with this request could not be found."
            });
        

        var (isValid, isGif) = await IsImageValidAsync(formFile);

        if (!isValid)
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Image",
                Detail = "The uploaded file is either corrupted, empty, or not a supported image format. Please upload a valid image file."
            });

        var extension = Path.GetExtension(formFile.FileName)?.TrimStart('.');
        var value = string.IsNullOrWhiteSpace(extension) ? "png" : extension;

        var file = await GenerateUrlAsync(user);

        var result = await _awsS3.UploadImageAsync("users", file, formFile);
        
        if (!result)
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Upload Failed",
                Detail = "An error occurred while uploading the image. Please try again later."
            });
        
        user.AvatarUrl = $"file/user/{file}";

        await SaveUserAndUpdate(user);
            
        return this.ApiResponseAccepted<AvatarResponse>(title: "Avatar successfully updated", 
            data: new()
            {
                AvatarUrl = $"file/user/{file}",
                ChangedAt = DateTime.UtcNow
            });
    }


    private async Task<(bool isValid, bool isGif)> IsImageValidAsync(IFormFile file)
    {
        try
        {
            using var stream = file.OpenReadStream();
            using var image = await Image.LoadAsync(stream);

            bool isGif =
                image.Metadata.DecodedImageFormat!.Name.Equals("GIF", StringComparison.OrdinalIgnoreCase) ||
                image.Metadata.DecodedImageFormat.Name.Equals("WEBP", StringComparison.OrdinalIgnoreCase) || 
                image.Metadata.DecodedImageFormat.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase);

            var isValid = file.Length <= 30 * 1024 * 1024;

            return (isValid, isGif);
        }
        catch
        {
            return (false, false);
        }
    }

    private Task<string> GenerateUrlAsync(MongoUser user)
    {
        var baseUrl = $"{user.Id}-{user.Code}-{user.Name}";

        using var sha256 = SHA256.Create();
        
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(baseUrl));
        
        var builder = new StringBuilder();
        
        foreach (var @byte in bytes)
            builder.Append(@byte.ToString("x2"));
        
        return Task.FromResult($"{builder}/avatar.webp");
    }
}
