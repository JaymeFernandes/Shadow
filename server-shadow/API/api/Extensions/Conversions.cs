using Application.DTOs.Admin.User;
using Application.DTOs.Profile;
using Domain.Models.User;

namespace Api.Extensions;

public static class Conversions
{
    public static AllUserDto.Response.User ToDtoAdmin(this MongoUser user)
        => new()
        {
            Id = user.Code,
            AvatarUrl = user.AvatarUrl,
            Name = user.Name,
            Display = user.Display,
            Created = user.CachedAt,
            IsPrivate = user.IsPrivate,
            Lang = user.Lang,
            LastLogin = user.LastLoginAt
        };

    public static ProfileDto.Get.Response ToDtoProfile(this MongoUser user)
        => new()
        {
            Display = user.Display,
            Name = user.Name,
            AvatarUrl = user.AvatarUrl,
            IsPrivate = user.IsPrivate,
            Lang = user.Lang,
            LastLoginAt = DateTime.UtcNow
        };
}