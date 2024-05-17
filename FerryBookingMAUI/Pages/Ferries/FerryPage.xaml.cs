using FerryBookingClassLibrary.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    public partial class FerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();

        public ICommand CreateFerryCommand { get; }
        public ICommand DetailsCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public FerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            CreateFerryCommand = new Command(async () => await CreateFerry());
            DetailsCommand = new Command<Ferry>(async (ferry) => await ShowDetails(ferry));
            EditCommand = new Command<Ferry>(async (ferry) => await EditFerry(ferry));
            DeleteCommand = new Command<Ferry>(async (ferry) => await DeleteFerry(ferry));

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerries();
        }

        private async Task LoadFerries()
        {
            var ferries = await _ferryService.GetFerriesAsync();
            Ferries.Clear();
            foreach (var ferry in ferries)
            {
                Ferries.Add(ferry);
            }
        }

        private async Task CreateFerry()
        {
            await Shell.Current.GoToAsync(nameof(CreateFerryPage));
        }

        private async Task ShowDetails(Ferry ferry)
        {
            await Shell.Current.GoToAsync($"{nameof(FerryDetailsPage)}?FerryId={ferry.Id}");
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
