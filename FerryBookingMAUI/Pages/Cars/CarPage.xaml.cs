using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    public partial class CarPage : ContentPage
    {
        private readonly CarService _carService;

        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

        public ICommand CreateCarCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public CarPage(CarService carService)
        {
            InitializeComponent();
            _carService = carService;

            CreateCarCommand = new Command(async () => await CreateCar());
            DetailsCommand = new Command<Car>(async (car) => await ShowDetails(car));
            EditCommand = new Command<Car>(async (car) => await EditCar(car));
            DeleteCommand = new Command<Car>(async (car) => await DeleteCar(car));

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCars();
        }

        private async Task LoadCars()
        {
            var cars = await _carService.GetCarsAsync();
            Cars.Clear();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }

        private async Task CreateCar()
        {
            // Navigate to CreateCarPage (not implemented here)
        }

        private async Task ShowDetails(Car car)
        {
            // Navigate to CarDetailsPage (not implemented here)
        }

        private async Task EditCar(Car car)
        {
            // Navigate to EditCarPage (not implemented here)
        }

        private async Task DeleteCar(Car car)
        {
            await _carService.DeleteCarAsync(car.Id);
            await LoadCars();
        }
    }
}