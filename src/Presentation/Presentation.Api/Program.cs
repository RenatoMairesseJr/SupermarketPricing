using Infrastructure.Application.DependencyInjection;
using Infrastructure.Application.Identity;
using Infrastructure.Application.Swagger;
using Infrastructure.Middleware.Exception;
using Infrastructure.Middleware.ContentSecurityPolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var frontEndList = builder.Configuration["FrontEndUrls"]?.Split(';') ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsBuilder => corsBuilder
                           .WithOrigins(frontEndList)
                           .AllowAnyMethod()
                           .WithExposedHeaders("content-security-policy", "Content-Lenth")
                           .AllowCredentials()
                           .AllowAnyHeader());
});

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.SetupIoC();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentService(builder.Configuration);

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseSwagger();

if (app.Environment.IsDevelopment())
    app.UseSwaggerUI();
else
    app.AddCsp();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();