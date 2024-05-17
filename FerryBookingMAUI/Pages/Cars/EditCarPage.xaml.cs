using FerryBookingClassLibrary.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    [QueryProperty(nameof(CarId), nameof(CarId))]
    public partial class EditCarPage : ContentPage
    {
        private readonly CarService _carService;
        private readonly FerryService _ferryService;
        private readonly GuestService _guestService;

        public int CarId { get; set; }

        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();
        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();

        public Ferry SelectedFerry { get; set; }
        public ObservableCollection<Guest> SelectedGuests { get; set; } = new ObservableCollection<Guest>();

        public ICommand SaveCommand { get; }

        public EditCarPage(CarService carService, FerryService ferryService, GuestService guestService)
        {
            InitializeComponent();
            _carService = carService;
            _ferryService = ferryService;
            _guestService = guestService;

            SaveCommand = new Command(async () => await SaveCar());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            var ferries = await _ferryService.GetFerriesAsync();
            Ferries.Clear();
            foreach (var ferry in ferries)
            {
                Ferries.Add(ferry);
            }

            var guests = await _guestService.GetGuestsAsync();
            Guests.Clear();
            foreach (var guest in guests)
            {
                Guests.Add(guest);
            }

            var car = await _carService.GetCarByIdAsync(CarId);
            SelectedFerry = Ferries.FirstOrDefault(f => f.Id == car.FerryId);
            foreach (var guest in car.Guests)
            {
                SelectedGuests.Add(Guests.First(g => g.Id == guest.Id));
            }
        }

        private async Task SaveCar()
        {
            var car = new Car
            {
                Id = CarId,
                FerryId = SelectedFerry.Id,
                Guests = SelectedGuests.ToList()
            };

            await _carService.UpdateCarAsync(CarId, car);
            await Navigation.PopAsync();
        }
    }
}
