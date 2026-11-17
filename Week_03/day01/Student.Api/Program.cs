using System.Diagnostics;
using StudentManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.Use(
    async (context, next) =>
    {
        app.Logger.LogInformation(
            "Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path
        );
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            stopwatch.Stop();

            app.Logger.LogInformation(
                "Request Duration: {DurationMs} ms",
                stopwatch.ElapsedMilliseconds
            );
        }
        app.Logger.LogInformation("Status Code: {StatusCode}", context.Response.StatusCode);
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
