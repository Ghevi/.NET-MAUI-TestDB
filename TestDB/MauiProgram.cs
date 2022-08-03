using Microsoft.EntityFrameworkCore;
using TestDB.Entities;

namespace TestDB;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddScoped<MyDbService>();

        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "myDb");
        builder.Services.AddDbContext<MyDbContext>(opts =>
        {
            opts.UseSqlite($"FileName={path}");
        });

        var app = builder.Build();

        var dbc = app.Services.GetRequiredService<MyDbContext>();
        dbc.Database.Migrate();

        return app;
    }
}
