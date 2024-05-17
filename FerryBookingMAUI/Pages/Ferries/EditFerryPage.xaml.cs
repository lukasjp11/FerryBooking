using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages
{
    [QueryProperty(nameof(FerryId), nameof(FerryId))]
    public partial class EditFerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        public int FerryId { get; set; }

        public Ferry Ferry { get; set; } = new Ferry();

        public ICommand SaveCommand { get; }

        public EditFerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            SaveCommand = new Command(async () => await SaveFerry());

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

        private async Task SaveFerry()
        {
            await _ferryService.UpdateFerryAsync(FerryId, Ferry);
            await Navigation.PopAsync();
        }
    }
}