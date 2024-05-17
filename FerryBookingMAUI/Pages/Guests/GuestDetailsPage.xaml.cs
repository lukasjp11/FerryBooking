using FerryBookingClassLibrary.Models;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    [QueryProperty(nameof(GuestId), nameof(GuestId))]
    public partial class GuestDetailsPage : ContentPage
    {
        private readonly GuestService _guestService;

        public int GuestId { get; set; }

        public Guest Guest { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public GuestDetailsPage(GuestService guestService)
        {
            InitializeComponent();
            _guestService = guestService;

            EditCommand = new Command(async () => await EditGuest());
            DeleteCommand = new Command(async () => await DeleteGuest());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadGuest();
        }

        private async Task LoadGuest()
        {
            Guest = await _guestService.GetGuestByIdAsync(GuestId);
        }

        private async Task EditGuest()
        {
            await Shell.Current.GoToAsync($"EditGuestPage?GuestId={GuestId}");
        }

        private async Task DeleteGuest()
        {
            await _guestService.DeleteGuestAsync(GuestId);
            await Shell.Current.GoToAsync("..");
        }
    }
}