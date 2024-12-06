using DataGridView.Contracts;
using DataGridView.Database;
using DataGridView.ProductManager;
using Serilog;

namespace DataGridViewWebApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341", apiKey: "y7yJHv4SMo1xIrNwP68O")
                .CreateLogger();

            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IProductStorage, ProductStorage>();
            builder.Services.AddScoped<IProductManager, ProductsManager>();
            builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                "default",
                "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
