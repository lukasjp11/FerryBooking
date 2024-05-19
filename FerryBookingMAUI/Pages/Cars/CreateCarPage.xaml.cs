using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Cars
{
    public partial class CreateCarPage : ContentPage
    {
        private readonly CarService _carService;
        private readonly FerryService _ferryService;
        private readonly GuestService _guestService;

        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();
        public ObservableCollection<Guest> AvailableGuests { get; set; } = new ObservableCollection<Guest>();

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
                    _ = UpdateAvailableGuests();
                }
                ValidateSelectedFerry();
            }
        }

        public ObservableCollection<Guest> SelectedGuests { get; set; } = new ObservableCollection<Guest>();

        public ICommand CreateCommand { get; }

        public string FerryError { get; set; }
        public bool IsFerryErrorVisible => !string.IsNullOrEmpty(FerryError);

        public string GuestsError { get; set; }
        public bool IsGuestsErrorVisible => !string.IsNullOrEmpty(GuestsError);

        public CreateCarPage(CarService carService, FerryService ferryService, GuestService guestService)
        {
            InitializeComponent();
            _carService = carService;
            _ferryService = ferryService;
            _guestService = guestService;

            CreateCommand = new Command(async () => await CreateCar());

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

            if (Ferries.Count > 0)
            {
                SelectedFerry = Ferries.First();
            }

            await UpdateAvailableGuests();
        }

        private async Task UpdateAvailableGuests()
        {
            if (SelectedFerry == null) return;

            var guests = await _guestService.GetGuestsByFerryAsync(SelectedFerry.Id);
            var cars = await _carService.GetCarsAsync();

            var assignedGuests = cars.SelectMany(car => car.Guests).Select(g => g.Id).ToHashSet();

            AvailableGuests.Clear();
            foreach (var guest in guests)
            {
                if (!assignedGuests.Contains(guest.Id))
                {
                    AvailableGuests.Add(guest);
                }
            }

            ValidateSelectedFerry();
        }

        private async Task CreateCar()
        {

            if (SelectedGuests.Count < 1 || SelectedGuests.Count > 5)
            {
                GuestsError = "The car must have at least 1 guest and a maximum of 5 guests.";
                OnPropertyChanged(nameof(GuestsError));
                OnPropertyChanged(nameof(IsGuestsErrorVisible));
                return;
            }

            var car = new Car
            {
                FerryId = SelectedFerry.Id,
                Guests = SelectedGuests.ToList()
            };

            await _carService.CreateCarAsync(car);
            await Navigation.PopAsync();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGuests.Clear();
            foreach (var item in e.CurrentSelection)
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
