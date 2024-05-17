using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    public partial class GuestPage : ContentPage
    {
        private readonly GuestService _guestService;

        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();

        public ICommand CreateGuestCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public GuestPage(GuestService guestService)
        {
            InitializeComponent();
            _guestService = guestService;

            CreateGuestCommand = new Command(async () => await CreateGuest());
            DetailsCommand = new Command<Guest>(async (guest) => await ShowDetails(guest));
            EditCommand = new Command<Guest>(async (guest) => await EditGuest(guest));
            DeleteCommand = new Command<Guest>(async (guest) => await DeleteGuest(guest));

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadGuests();
        }

        private async Task LoadGuests()
        {
            var guests = await _guestService.GetGuestsAsync();
            Guests.Clear();
            foreach (var guest in guests)
            {
                Guests.Add(guest);
            }
        }

        private async Task CreateGuest()
        {
            await Shell.Current.GoToAsync(nameof(CreateGuestPage));
        }

        private async Task ShowDetails(Guest guest)
        {
            await Shell.Current.GoToAsync($"{nameof(GuestDetailsPage)}?GuestId={guest.Id}");
        }

        private async Task EditGuest(Guest guest)
        {
            await Shell.Current.GoToAsync($"{nameof(EditGuestPage)}?GuestId={guest.Id}");
        }

        private async Task DeleteGuest(Guest guest)
        {
            await _guestService.DeleteGuestAsync(guest.Id);
            await LoadGuests();
        }
    }
}
