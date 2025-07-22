using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class ApiResponse<T>
{
    public int Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Meta { get; set; }
     

    public ApiResponse(int status, string? title, T? data, object? meta = null)
    { 
        Status = status;
        Title = title;
        Data = data;
        Meta = meta;
    }
}

public static class ApiResponse
{
    public static IActionResult ApiResponseOk<T>(this ControllerBase ctrl, string? title = null, T? data = default, object? meta = null) =>
        ctrl.Ok(new ApiResponse<T>(200, title, data) { Meta = meta });

    public static IActionResult ApiResponseCreated<T>(this ControllerBase ctrl, string? title = null, T? data = default, object? meta = null) =>
        ctrl.StatusCode(201, new ApiResponse<T>(StatusCodes.Status201Created, title, data) { Meta = meta });

    public static IActionResult ApiResponseAccepted<T>(this ControllerBase ctrl, string? title = null, T? data = default, object? meta = null) =>
        ctrl.StatusCode(202, new ApiResponse<T>(StatusCodes.Status202Accepted, title, data) { Meta = meta });
    
}

