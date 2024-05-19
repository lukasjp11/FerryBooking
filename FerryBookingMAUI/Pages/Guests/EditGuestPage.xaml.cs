using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Guests
{
    [QueryProperty(nameof(GuestId), nameof(GuestId))]
    public partial class EditGuestPage : ContentPage
    {
        private readonly GuestService _guestService;
        private readonly FerryService _ferryService;

        public int GuestId { get; set; }

        public Guest Guest { get; set; } = new Guest();
        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();

        public Ferry SelectedFerry { get; set; }

        public ICommand SaveCommand { get; }

        public EditGuestPage(GuestService guestService, FerryService ferryService)
        {
            InitializeComponent();
            _guestService = guestService;
            _ferryService = ferryService;

            SaveCommand = new Command(async () => await SaveGuest());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerries();
            await LoadGuest();
        }

        private async Task LoadFerries()
        {
            var ferries = await _ferryService.GetFerriesAsync();
            Ferries.Clear();
            foreach (var ferry in ferries)
            {
                Ferries.Add(ferry);
            }
        }

        private async Task LoadGuest()
        {
            Guest = await _guestService.GetGuestByIdAsync(GuestId);
            SelectedFerry = Ferries.FirstOrDefault(f => f.Id == Guest.FerryId);
            // Gender should be directly bound and no need for conversion here
        }

        private async Task SaveGuest()
        {
            Guest.FerryId = SelectedFerry.Id;
            // Gender binding should directly save without conversion if bound properly
            await _guestService.UpdateGuestAsync(GuestId, Guest);
            await Navigation.PopAsync();
        }
    }
}