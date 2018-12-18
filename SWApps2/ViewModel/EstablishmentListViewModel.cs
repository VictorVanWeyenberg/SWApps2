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
using System.Net;
using Newtonsoft.Json;
using SWApps2.Converters;
using Windows.Web.Http;

namespace SWApps2.ViewModel
{
    public class EstablishmentListViewModel : ViewModelBase
    {
        const string url = "http://localhost:54100/api/Establishments";
        public ObservableCollection<EstablishmentViewModel> Establishments { get { return _establishments; } set { _establishments = value; } }
        public ObservableCollection<EstablishmentViewModel> FilteredEstablishments { get { return this._filteredEstablishments; } set { this._filteredEstablishments = value; } }
        private ObservableCollection<EstablishmentViewModel> _establishments;
        private ObservableCollection<EstablishmentViewModel> _filteredEstablishments;
        private string _lookupString = null;
        public string LookupString { get { return this._lookupString; } set {
                this._lookupString = value;
                if (this._lookupString != null)
                {
                    FilteredEstablishments = new ObservableCollection<EstablishmentViewModel>(Establishments.Where(est => est.Name.ToLower().Contains(this._lookupString) || est.Establishment.Tags.Exists(tag => tag.ToLower().Contains(this._lookupString))));
                } else
                {
                    FilteredEstablishments = Establishments;
                }
            } }

        public Action Callback { get; internal set; }

        public EstablishmentListViewModel()
        {
            _establishments = new ObservableCollection<EstablishmentViewModel>();
            _filteredEstablishments = _establishments;
            LoadData();
        }

        private async void LoadData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string jsonresult = await client.GetStringAsync(new Uri(url));
            DownloadCompleted(jsonresult);
            Callback();
        }

        private void DownloadCompleted(string json)
        {
            var establishments = JsonConvert.DeserializeObject<Establishment[]>(json, new EstablishmentJsonConverter());
            foreach (Establishment establishment in establishments)
            {
                foreach (EstablishmentEvent eventje in establishment.EstablishmentEvents)
                {
                    eventje.Establishment = establishment;
                }
            }
            Establishments.Clear();
            foreach (Establishment establishment in establishments)
            {
                EstablishmentViewModel evm = new EstablishmentViewModel
                {
                    Establishment = establishment
                };
                Establishments.Add(evm);
            }
        }
    }
}
