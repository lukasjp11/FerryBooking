using FerryBookingMAUI.Pages.Cars;
using FerryBookingMAUI.Pages.Ferries;
using FerryBookingMAUI.Pages.Guests;
using FerryBookingMAUI.Services;

namespace FerryBookingMAUI
{
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

            builder.Services.AddHttpClient<CarService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7163/");
            });
            builder.Services.AddHttpClient<FerryService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7163/");
            });
            builder.Services.AddHttpClient<GuestService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7163/");
            });

            builder.Services.AddSingleton<CarPage>();
            builder.Services.AddSingleton<FerryPage>();
            builder.Services.AddSingleton<GuestPage>();
            builder.Services.AddTransient<CreateCarPage>();
            builder.Services.AddTransient<EditCarPage>();
            builder.Services.AddTransient<CreateFerryPage>();
            builder.Services.AddTransient<EditFerryPage>();
            builder.Services.AddTransient<CreateGuestPage>();
            builder.Services.AddTransient<EditGuestPage>();

            return builder.Build();
        }
    }
}
