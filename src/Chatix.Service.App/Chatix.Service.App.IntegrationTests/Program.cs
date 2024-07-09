
using Chatix.Libs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<RepositoryChatixDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryDbForTesting"));

        builder.Services.AddMediatR(typeof(Program).Assembly);
        builder.Services.AddSignalR();

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