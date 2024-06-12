using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Ferries
{
    public partial class FerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        public FerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            CreateFerryCommand = new Command(async () => await CreateFerry());
            EditCommand = new Command<Ferry>(async ferry => await EditFerry(ferry));
            DeleteCommand = new Command<Ferry>(async ferry => await DeleteFerry(ferry));

            BindingContext = this;
        }

        public ObservableCollection<Ferry> Ferries { get; set; } = new();

        public ICommand CreateFerryCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerries();
        }

        private async Task LoadFerries()
        {
            IEnumerable<Ferry> ferries = await _ferryService.GetFerriesAsync();
            Ferries.Clear();
            foreach (Ferry ferry in ferries)
            {
                Ferries.Add(ferry);
            }
        }

        private async Task CreateFerry()
        {
            await Shell.Current.GoToAsync(nameof(CreateFerryPage));
        }

        private async Task EditFerry(Ferry ferry)
        {
            await Shell.Current.GoToAsync($"{nameof(EditFerryPage)}?FerryId={ferry.Id}");
        }

        private async Task DeleteFerry(Ferry ferry)
        {
            await _ferryService.DeleteFerryAsync(ferry.Id);
            await LoadFerries();
        }
    }
}