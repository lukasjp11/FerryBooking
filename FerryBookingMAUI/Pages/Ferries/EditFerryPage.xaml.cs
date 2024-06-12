using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Helpers;
using FerryBookingMAUI.Services;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Ferries
{
    [QueryProperty(nameof(FerryId), nameof(FerryId))]
    public partial class EditFerryPage : ContentPage
    {
        private readonly FerryService _ferryService;

        private Ferry _ferry;

        public EditFerryPage(FerryService ferryService)
        {
            InitializeComponent();
            _ferryService = ferryService;

            SaveCommand = new Command(async () => await SaveFerry());

            BindingContext = this;
        }

        public int FerryId { get; set; }

        public Ferry Ferry
        {
            get => _ferry;
            set
            {
                _ferry = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

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
            if (ValidateFerry())
            {
                await _ferryService.UpdateFerryAsync(FerryId, Ferry);
                await Navigation.PopAsync();
            }
        }

        private bool ValidateFerry()
        {
            bool isValid = ValidatorHelper.TryValidateObject(Ferry, out List<ValidationResult> validationResults);

            NameError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.Name)))?.ErrorMessage;
            MaxCarsError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.MaxCars)))?.ErrorMessage;
            MaxGuestsError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.MaxGuests)))
                ?.ErrorMessage;
            PricePerCarError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.PricePerCar)))
                ?.ErrorMessage;
            PricePerGuestError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Ferry.PricePerGuest)))
                ?.ErrorMessage;

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