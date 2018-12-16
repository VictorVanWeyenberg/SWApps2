using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWApps2.Data;
using SWApps2.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using SWApps2.Converters;
using System.Net.Http;
using Windows.UI.Xaml;

namespace SWApps2.ViewModel
{
    public class EstablishmentListViewModel : ViewModelBase
    {
        const string url = "http://localhost:54100/api/establishment";

        private ObservableCollection<EstablishmentListViewItemViewModel> _items;

        public ReadOnlyObservableCollection<EstablishmentListViewItemViewModel> Items { get { return new ReadOnlyObservableCollection<EstablishmentListViewItemViewModel>(_items); } }

        public Establishment SelectedEstablishment { get; private set; }

        public bool CanShowExtraOptions { get; private set; }

        public EstablishmentListViewModel()
        {
            _items = new ObservableCollection<EstablishmentListViewItemViewModel>();
            LoadData();
        }

        private async void LoadData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string jsonresult = await client.GetStringAsync(new Uri(url));
            DownloadCompleted(jsonresult);
        }

        private void DownloadCompleted(string json)
        {
            var app = Application.Current as App;
            CanShowExtraOptions = app.User == null || app.User is Entrepreneur ? false : true;
            var establishments = JsonConvert.DeserializeObject<Establishment[]>(json, new EstablishmentJsonConverter2());
            _items.Clear();
            foreach (Establishment establishment in establishments)
            {
                _items.Add(new EstablishmentListViewItemViewModel {
                    Establishment = establishment,
                    CanShowMenu = CanShowExtraOptions
                });
            }
        }
    }
}
