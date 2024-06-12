using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Guests
{
    public partial class GuestPage : ContentPage
    {
        private readonly GuestService _guestService;

        public GuestPage(GuestService guestService)
        {
            InitializeComponent();
            _guestService = guestService;

            CreateGuestCommand = new Command(async () => await CreateGuest());
            EditCommand = new Command<Guest>(async guest => await EditGuest(guest));
            DeleteCommand = new Command<Guest>(async guest => await DeleteGuest(guest));

            BindingContext = this;
        }

        public ObservableCollection<Guest> Guests { get; set; } = new();

        public ICommand CreateGuestCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadGuests();
        }

        private async Task LoadGuests()
        {
            IEnumerable<Guest> guests = await _guestService.GetGuestsAsync();
            Guests.Clear();
            foreach (Guest guest in guests)
            {
                Guests.Add(guest);
            }
        }

        private async Task CreateGuest()
        {
            await Shell.Current.GoToAsync(nameof(CreateGuestPage));
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