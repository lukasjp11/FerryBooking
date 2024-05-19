using FerryBookingClassLibrary.Models;
using FerryBookingMAUI.Helpers;
using FerryBookingMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FerryBookingMAUI.Pages.Guests
{
    public partial class CreateGuestPage : ContentPage
    {
        private readonly GuestService _guestService;
        private readonly FerryService _ferryService;

        private Guest _guest = new Guest();
        public Guest Guest
        {
            get => _guest;
            set
            {
                _guest = value;
                OnPropertyChanged(nameof(Guest));
            }
        }

        public ObservableCollection<Ferry> Ferries { get; set; } = new ObservableCollection<Ferry>();

        private Ferry _selectedFerry;
        public Ferry SelectedFerry
        {
            get => _selectedFerry;
            set
            {
                _selectedFerry = value;
                OnPropertyChanged(nameof(SelectedFerry));
            }
        }

        private string _selectedGender;
        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                Guest.Gender = _selectedGender == "Female";
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        public ICommand CreateCommand { get; }

        public string NameError { get; set; }
        public string GenderError { get; set; }
        public string FerryError { get; set; }

        public bool IsNameErrorVisible => !string.IsNullOrEmpty(NameError);
        public bool IsGenderErrorVisible => !string.IsNullOrEmpty(GenderError);
        public bool IsFerryErrorVisible => !string.IsNullOrEmpty(FerryError);

        public CreateGuestPage(GuestService guestService, FerryService ferryService)
        {
            InitializeComponent();
            _guestService = guestService;
            _ferryService = ferryService;

            CreateCommand = new Command(async () => await CreateGuest());

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

        private async Task CreateGuest()
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
                await _guestService.CreateGuestAsync(Guest);
                await Navigation.PopAsync();
            }
        }

        private bool ValidateGuest()
        {
            var isValid = ValidatorHelper.TryValidateObject(Guest, out var validationResults);

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
