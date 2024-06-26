using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Cars
{
    public partial class CarPage : ContentPage
    {
        private readonly CarService _carService;

        public CarPage(CarService carService)
        {
            InitializeComponent();
            _carService = carService;

            CreateCarCommand = new Command(async () => await CreateCar());
            EditCommand = new Command<Car>(async car => await EditCar(car));
            DeleteCommand = new Command<Car>(async car => await DeleteCar(car));

            BindingContext = this;
        }

        public ObservableCollection<Car> Cars { get; set; } = new();

        public ICommand CreateCarCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCars();
        }

        private async Task LoadCars()
        {
            IEnumerable<Car> cars = await _carService.GetCarsAsync();
            Cars.Clear();
            foreach (Car car in cars)
            {
                Cars.Add(car);
            }
        }

        private async Task CreateCar()
        {
            await Shell.Current.GoToAsync(nameof(CreateCarPage));
        }

        private async Task EditCar(Car car)
        {
            await Shell.Current.GoToAsync($"{nameof(EditCarPage)}?CarId={car.Id}");
        }

        private async Task DeleteCar(Car car)
        {
            await _carService.DeleteCarAsync(car.Id);
            await LoadCars();
        }
    }
}