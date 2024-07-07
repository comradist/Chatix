using Chatix.Libs.Infrastructure.Persistence;
using Chatix.Libs.Infrastructure.Logger;
using Chatix.Service.App.Domain;
using Chatix.Service.App.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure the services
builder.Services.ConfigureDomainServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.ConfigureValidationFilterAttribute();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.ConfigureCQRS();
builder.Services.ConfigureSwagger();

builder.Services.AddControllers()
.AddApplicationPart(typeof(Chatix.Service.App.API.Presentation.AssemblyReference).Assembly)
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
