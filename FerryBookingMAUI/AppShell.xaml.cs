﻿using FerryBookingMAUI.Pages;

namespace FerryBookingMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CreateCarPage), typeof(CreateCarPage));
            Routing.RegisterRoute(nameof(EditCarPage), typeof(EditCarPage));
            Routing.RegisterRoute(nameof(CreateFerryPage), typeof(CreateFerryPage));
            Routing.RegisterRoute(nameof(EditFerryPage), typeof(EditFerryPage));
            Routing.RegisterRoute(nameof(CreateGuestPage), typeof(CreateGuestPage));
            Routing.RegisterRoute(nameof(EditGuestPage), typeof(EditGuestPage));
        }
    }
}
