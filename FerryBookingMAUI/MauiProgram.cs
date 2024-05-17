using FerryBookingMAUI.Pages;
using FerryBookingMAUI.Services;

namespace FerryBookingMAUI;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services
        builder.Services.AddSingleton<CarService>();
        builder.Services.AddSingleton<FerryService>();
        builder.Services.AddSingleton<GuestService>();

        // Register pages
        builder.Services.AddSingleton<CarPage>();
        builder.Services.AddSingleton<FerryPage>();
        builder.Services.AddSingleton<GuestPage>();
        builder.Services.AddTransient<CreateCarPage>();
        builder.Services.AddTransient<EditCarPage>();
        builder.Services.AddTransient<CarDetailsPage>();
        builder.Services.AddTransient<CreateFerryPage>();
        builder.Services.AddTransient<EditFerryPage>();
        builder.Services.AddTransient<FerryDetailsPage>();
        builder.Services.AddTransient<CreateGuestPage>();
        builder.Services.AddTransient<EditGuestPage>();
        builder.Services.AddTransient<GuestDetailsPage>();

        return builder.Build();
    }
}