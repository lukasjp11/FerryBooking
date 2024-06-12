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

        private Ferry _selectedFerry;

        public EditCarPage(CarService carService, FerryService ferryService, GuestService guestService)
        {
            InitializeComponent();
            _carService = carService;
            _ferryService = ferryService;
            _guestService = guestService;

            SaveCommand = new Command(async () => await SaveCar());

            BindingContext = this;
        }

        public int CarId { get; set; }

        public ObservableCollection<Ferry> Ferries { get; set; } = new();
        public ObservableCollection<Guest> AvailableGuests { get; set; } = new();

        public Ferry SelectedFerry
        {
            get => _selectedFerry;
            set
            {
                _selectedFerry = value;
                OnPropertyChanged();
                if (_selectedFerry != null)
                {
                    _ = UpdateAvailableGuests();
                }

                ValidateSelectedFerry();
            }
        }

        public ObservableCollection<Guest> SelectedGuests { get; set; } = new();

        public ICommand SaveCommand { get; }

        public string FerryError { get; set; }
        public bool IsFerryErrorVisible => !string.IsNullOrEmpty(FerryError);

        public string GuestsError { get; set; }
        public bool IsGuestsErrorVisible => !string.IsNullOrEmpty(GuestsError);

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            IEnumerable<Ferry> ferries = await _ferryService.GetFerriesAsync();
            Ferries.Clear();
            foreach (Ferry ferry in ferries)
            {
                Ferries.Add(ferry);
            }

            Car? car = await _carService.GetCarByIdAsync(CarId);
            if (car != null)
            {
                SelectedFerry = Ferries.FirstOrDefault(f => f.Id == car.FerryId);
                SelectedGuests.Clear();
                foreach (Guest guest in car.Guests)
                {
                    SelectedGuests.Add(guest);
                }
            }

            await UpdateAvailableGuests();
        }

        private async Task UpdateAvailableGuests()
        {
            if (SelectedFerry == null)
            {
                return;
            }

            IEnumerable<Guest> guests = await _guestService.GetGuestsByFerryAsync(SelectedFerry.Id);
            IEnumerable<Car> cars = await _carService.GetCarsAsync();

            HashSet<int> assignedGuests = cars.Where(c => c.Id != CarId).SelectMany(car => car.Guests).Select(g => g.Id)
                .ToHashSet();

            AvailableGuests.Clear();
            foreach (Guest guest in guests)
            {
                if (!assignedGuests.Contains(guest.Id) || SelectedGuests.Any(g => g.Id == guest.Id))
                {
                    AvailableGuests.Add(guest);
                }
            }

            ValidateSelectedFerry();
        }

        private async Task SaveCar()
        {
            if (SelectedGuests.Count < 1 || SelectedGuests.Count > 5)
            {
                GuestsError = "The car must have at least 1 guest and a maximum of 5 guests.";
                OnPropertyChanged(nameof(GuestsError));
                OnPropertyChanged(nameof(IsGuestsErrorVisible));
                return;
            }

            Car car = new Car { Id = CarId, FerryId = SelectedFerry.Id, Guests = SelectedGuests.ToList() };

            await _carService.UpdateCarAsync(CarId, car);
            await Navigation.PopAsync();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGuests.Clear();
            foreach (object? item in e.CurrentSelection)
            {
                SelectedGuests.Add(item as Guest);
            }
        }

        private void ValidateSelectedFerry()
        {
            FerryError = AvailableGuests.Count == 0 ? "The selected ferry has no guests." : null;

            OnPropertyChanged(nameof(FerryError));
            OnPropertyChanged(nameof(IsFerryErrorVisible));
        }
    }
}