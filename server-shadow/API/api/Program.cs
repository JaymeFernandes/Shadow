using Api.Extensions;
using Hellang.Middleware.ProblemDetails;
using Infrastructure.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddProblemDetailsSetup();

builder.Services
    .AddAuth(builder.Configuration)
    .AddMongoDbSetup(builder.Configuration)
    .AddEfCoreSetup(builder.Configuration)
    .AddScoped<AuthServices>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseProblemDetails();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseAuth();

app.Run();
