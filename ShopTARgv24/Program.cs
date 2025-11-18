using Microsoft.EntityFrameworkCore;
using ShopTARgv24.ApplicationServices.Services;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Data;

namespace ShopTARgv24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
            builder.Services.AddScoped<IFileServices, FileServices>();
            builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
            builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();

            builder.Services.AddDbContext<ShopTARgv24Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient<ICocktailService, CocktailService>(client =>
            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
            });

            builder.Services.AddHttpClient<IChuckNorrisServices, ChuckNorrisServices>();

            var app = builder.Build();

            // Автоматическое применение миграций при старте приложения
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ShopTARgv24Context>();
                dbContext.Database.Migrate();  // Применяет все миграции, если они не были применены ранее
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
