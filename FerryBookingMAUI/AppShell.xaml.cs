using FerryBookingMAUI.Pages;

namespace FerryBookingMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateCarPage), typeof(CreateCarPage));
            Routing.RegisterRoute(nameof(EditCarPage), typeof(EditCarPage));
            Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
            Routing.RegisterRoute(nameof(CreateFerryPage), typeof(CreateFerryPage));
            Routing.RegisterRoute(nameof(EditFerryPage), typeof(EditFerryPage));
            Routing.RegisterRoute(nameof(FerryDetailsPage), typeof(FerryDetailsPage));
            Routing.RegisterRoute(nameof(CreateGuestPage), typeof(CreateGuestPage));
            Routing.RegisterRoute(nameof(EditGuestPage), typeof(EditGuestPage));
            Routing.RegisterRoute(nameof(GuestDetailsPage), typeof(GuestDetailsPage));
        }
    }
}
