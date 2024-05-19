using FerryBookingClassLibrary.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Guests
{
    public partial class CreateGuestPage : ContentPage
    {
        private readonly GuestService _guestService;
        private readonly FerryService _ferryService;

        public Guest Guest { get; set; } = new Guest();
        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();

        public Ferry SelectedFerry { get; set; }

        public ICommand CreateCommand { get; }

        public CreateGuestPage(GuestService guestService, FerryService ferryService)
        {
            InitializeComponent();
            _guestService = guestService;
            _ferryService = ferryService;

            CreateCommand = new Command(async () => await CreateGuest());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerries();
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

        private async Task CreateGuest()
        {
            Guest.FerryId = SelectedFerry.Id;
            Guest.Gender = Guest.Gender.Equals("Female");

            await _guestService.CreateGuestAsync(Guest);
            await Navigation.PopAsync();
        }
    }
}