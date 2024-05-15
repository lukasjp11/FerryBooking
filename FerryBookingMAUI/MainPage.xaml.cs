using FerryBookingMAUI.ViewModels;

namespace FerryBookingMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new FerryViewModel();
        }
    }

}
