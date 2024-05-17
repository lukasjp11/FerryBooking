using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    [QueryProperty(nameof(FerryId), nameof(FerryId))]
    public partial class FerryDetailsPage : ContentPage
    {
        private readonly FerryService _ferryService;

        public int FerryId { get; set; }

        public Ferry Ferry { get; set; }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public FerryDetailsPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            EditCommand = new Command(async () => await EditFerry());
            DeleteCommand = new Command(async () => await DeleteFerry());

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerry();
        }

        private async Task LoadFerry()
        {
            Ferry = await _ferryService.GetFerryByIdAsync(FerryId);
        }

        private async Task EditFerry()
        {
            await Shell.Current.GoToAsync($"EditFerryPage?FerryId={FerryId}");
        }

        private async Task DeleteFerry()
        {
            await _ferryService.DeleteFerryAsync(FerryId);
            await Shell.Current.GoToAsync("..");
        }
    }
}