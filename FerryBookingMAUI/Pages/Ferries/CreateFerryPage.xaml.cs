using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Helpers;
using FerryBookingMAUI.Services;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FerryBookingMAUI.Pages.Ferries
{
    public partial class CreateFerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        private Ferry _ferry = new Ferry();
        public Ferry Ferry
        {
            get => _ferry;
            set
            {
                _ferry = value;
                OnPropertyChanged(nameof(Ferry));
            }
        }

        public ICommand CreateCommand { get; }

        public string NameError { get; set; }
        public string MaxCarsError { get; set; }
        public string MaxGuestsError { get; set; }
        public string PricePerCarError { get; set; }
        public string PricePerGuestError { get; set; }

        public bool IsNameErrorVisible => !string.IsNullOrEmpty(NameError);
        public bool IsMaxCarsErrorVisible => !string.IsNullOrEmpty(MaxCarsError);
        public bool IsMaxGuestsErrorVisible => !string.IsNullOrEmpty(MaxGuestsError);
        public bool IsPricePerCarErrorVisible => !string.IsNullOrEmpty(PricePerCarError);
        public bool IsPricePerGuestErrorVisible => !string.IsNullOrEmpty(PricePerGuestError);

        public CreateFerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            CreateCommand = new Command(async () => await CreateFerry());

            BindingContext = this;
        }

        private async Task CreateFerry()
        {
            if (ValidateFerry())
            {
                await _ferryService.CreateFerryAsync(Ferry);
                await Navigation.PopAsync();
            }
        }

        private bool ValidateFerry()
        {
            var isValid = ValidatorHelper.TryValidateObject(Ferry, out var validationResults);

            NameError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.Name)))?.ErrorMessage;
            MaxCarsError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.MaxCars)))?.ErrorMessage;
            MaxGuestsError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.MaxGuests)))?.ErrorMessage;
            PricePerCarError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.PricePerCar)))?.ErrorMessage;
            PricePerGuestError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.PricePerGuest)))?.ErrorMessage;

            OnPropertyChanged(nameof(NameError));
            OnPropertyChanged(nameof(MaxCarsError));
            OnPropertyChanged(nameof(MaxGuestsError));
            OnPropertyChanged(nameof(PricePerCarError));
            OnPropertyChanged(nameof(PricePerGuestError));
            OnPropertyChanged(nameof(IsNameErrorVisible));
            OnPropertyChanged(nameof(IsMaxCarsErrorVisible));
            OnPropertyChanged(nameof(IsMaxGuestsErrorVisible));
            OnPropertyChanged(nameof(IsPricePerCarErrorVisible));
            OnPropertyChanged(nameof(IsPricePerGuestErrorVisible));

            return isValid;
        }
    }
}
