using FerryBookingClassLibrary.Models;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    [QueryProperty(nameof(CarId), nameof(CarId))]
    public partial class CarDetailsPage : ContentPage
    {
        private readonly CarService _carService;

        public int CarId { get; set; }

        public Car Car { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CarDetailsPage(CarService carService)
        {
            InitializeComponent();
            _carService = carService;

            EditCommand = new Command(async () => await EditCar());
            DeleteCommand = new Command(async () => await DeleteCar());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCar();
        }

        private async Task LoadCar()
        {
            Car = await _carService.GetCarByIdAsync(CarId);
        }

        private async Task EditCar()
        {
            await Shell.Current.GoToAsync($"EditCarPage?CarId={CarId}");
        }

        private async Task DeleteCar()
        {
            await _carService.DeleteCarAsync(CarId);
            await Shell.Current.GoToAsync("..");
        }
    }
}