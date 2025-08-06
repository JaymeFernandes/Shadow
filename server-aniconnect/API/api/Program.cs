using Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Infrastructure.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services
    .AddSwaggerSetup()
    .AddProblemDetailsSetup()
    .AddMeilisearchSetup(builder.Configuration)
    .AddS3Setup(builder.Configuration)
    .AddAuth(builder.Configuration)
    .AddMongoDbSetup(builder.Configuration)
    .AddEfCoreSetup(builder.Configuration)
    .AddScoped<AuthServices>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Api documentation");
    });
    
    
    app.MapScalarApiReference();
}

app.UseProblemDetails();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseAuth();

app.Run();
