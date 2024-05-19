using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Services;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Ferries
{
    public partial class CreateFerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        public Ferry Ferry { get; set; } = new Ferry();

        public ICommand CreateCommand { get; }

        public CreateFerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            CreateCommand = new Command(async () => await CreateFerry());

            BindingContext = this;
        }

        private async Task CreateFerry()
        {
            await _ferryService.CreateFerryAsync(Ferry);
            await Navigation.PopAsync();
        }
    }
}