using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Helpers;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace FerryBookingMAUI.Pages.Guests
{
    [QueryProperty(nameof(GuestId), nameof(GuestId))]
    public partial class EditGuestPage : ContentPage
    {
        private readonly FerryService _ferryService;
        private readonly GuestService _guestService;

        private Guest _guest = new();

        private Ferry _selectedFerry;

        private string _selectedGender;

        public EditGuestPage(GuestService guestService, FerryService ferryService)
        {
            InitializeComponent();
            _guestService = guestService;
            _ferryService = ferryService;

            SaveCommand = new Command(async () => await SaveGuest());

            BindingContext = this;
        }

        public int GuestId { get; set; }

        public Guest Guest
        {
            get => _guest;
            set
            {
                _guest = value;
                OnPropertyChanged();
                SelectedGender = _guest.Gender ? "Female" : "Male";
            }
        }

        public ObservableCollection<Ferry> Ferries { get; set; } = new();

        public Ferry SelectedFerry
        {
            get => _selectedFerry;
            set
            {
                _selectedFerry = value;
                OnPropertyChanged();
            }
        }

        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                Guest.Gender = _selectedGender == "Female";
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public string NameError { get; set; }
        public string GenderError { get; set; }
        public string FerryError { get; set; }

        public bool IsNameErrorVisible => !string.IsNullOrEmpty(NameError);
        public bool IsGenderErrorVisible => !string.IsNullOrEmpty(GenderError);
        public bool IsFerryErrorVisible => !string.IsNullOrEmpty(FerryError);

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadFerries();
            await LoadGuest();
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

        private async Task LoadGuest()
        {
            Guest = await _guestService.GetGuestByIdAsync(GuestId);
            SelectedFerry = Ferries.FirstOrDefault(f => f.Id == Guest.FerryId);
            SelectedGender = Guest.Gender ? "Female" : "Male";
        }

        private async Task SaveGuest()
        {
            if (SelectedFerry == null)
            {
                FerryError = "Please select a ferry.";
                OnPropertyChanged(nameof(FerryError));
                OnPropertyChanged(nameof(IsFerryErrorVisible));
                return;
            }

            Guest.FerryId = SelectedFerry.Id;

            if (ValidateGuest())
            {
                await _guestService.UpdateGuestAsync(GuestId, Guest);
                await Navigation.PopAsync();
            }
        }

        private bool ValidateGuest()
        {
            bool isValid = ValidatorHelper.TryValidateObject(Guest, out List<ValidationResult> validationResults);

            NameError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Guest.Name)))?.ErrorMessage;
            GenderError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Guest.Gender)))?.ErrorMessage;
            FerryError = validationResults.Find(vr => vr.MemberNames.Contains(nameof(Guest.FerryId)))?.ErrorMessage;

            OnPropertyChanged(nameof(NameError));
            OnPropertyChanged(nameof(GenderError));
            OnPropertyChanged(nameof(FerryError));
            OnPropertyChanged(nameof(IsNameErrorVisible));
            OnPropertyChanged(nameof(IsGenderErrorVisible));
            OnPropertyChanged(nameof(IsFerryErrorVisible));

            return isValid;
        }
    }
}