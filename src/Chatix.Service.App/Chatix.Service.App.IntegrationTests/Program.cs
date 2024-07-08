using Chatix.Libs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();

        // Add DbContext with configuration from appsettings.json
        builder.Services.AddDbContext<RepositoryChatixDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("SqlConnectionToAppDb"), new MySqlServerVersion(new Version(8, 0, 26))));

        // Register other services here (e.g., MediatR, SignalR, etc.)
        // builder.Services.AddMediatR(typeof(Program).Assembly);
        // builder.Services.AddSignalR();
        // builder.Services.AddScoped<ILoggerManager, LoggerManager>();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}