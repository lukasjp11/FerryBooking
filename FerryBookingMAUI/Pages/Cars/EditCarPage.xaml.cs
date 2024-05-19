using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Cars
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

        private Ferry _selectedFerry;
        public Ferry SelectedFerry
        {
            get => _selectedFerry;
            set
            {
                _selectedFerry = value;
                OnPropertyChanged();
                if (_selectedFerry != null)
                {
                    _ = UpdateGuests();
                }
            }
        }

        private ObservableCollection<Guest> _selectedGuests = new ObservableCollection<Guest>();
        public ObservableCollection<Guest> SelectedGuests
        {
            get => _selectedGuests;
            set
            {
                _selectedGuests = value;
                OnPropertyChanged();
            }
        }

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
            if (car != null)
            {
                SelectedFerry = Ferries.FirstOrDefault(f => f.Id == car.FerryId);
                SelectedGuests.Clear();
                foreach (var guest in car.Guests)
                {
                    SelectedGuests.Add(Guests.First(g => g.Id == guest.Id));
                }
            }
        }

        private async Task UpdateGuests()
        {
            var guests = await _guestService.GetGuestsByFerryAsync(SelectedFerry.Id);
            Guests.Clear();
            foreach (var guest in guests)
            {
                Guests.Add(guest);
            }
        }

        private async Task SaveCar()
        {
            if (SelectedGuests.Count < 1 || SelectedGuests.Count > 5)
            {
                await DisplayAlert("Error", "The car must have at least 1 guest and a maximum of 5 guests.", "OK");
                return;
            }

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
