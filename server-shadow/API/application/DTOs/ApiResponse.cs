using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Application.DTOs;

public class ApiResponse<T>
{
    public int Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }


    public object Meta { get; set; } = new {  };
    
    

    public ApiResponse(int status)
    {
        Status = status;
    }

    public ApiResponse(int status, T data)
    {
        Status = status;
        Data = data;
    }

    public ApiResponse(int status, string title)
    {
        Status = status;
        Title = title;
    }

    public ApiResponse(int status, string title, T data)
    {
        Status = status;
        Title = title;
        Data = data;
    }
}