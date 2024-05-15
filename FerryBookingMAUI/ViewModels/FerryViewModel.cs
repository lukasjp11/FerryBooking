using FerryBookingClassLibrary.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace FerryBookingMAUI.ViewModels
{
    public class FerryViewModel : INotifyPropertyChanged
    {
        private HttpClient _client;

        public ObservableCollection<Ferry> Ferries { get; set; }

        public FerryViewModel()
        {
            _client = new HttpClient();
            LoadFerries();
        }

        private async void LoadFerries()
        {
            var response = await _client.GetStringAsync("https://yourapiurl/api/ferryapi");
            var ferries = JsonConvert.DeserializeObject<List<Ferry>>(response);
            Ferries = new ObservableCollection<Ferry>(ferries);
            OnPropertyChanged(nameof(Ferries));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}