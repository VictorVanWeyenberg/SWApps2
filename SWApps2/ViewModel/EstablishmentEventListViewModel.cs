using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using SWApps2.Converters;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class EstablishmentEventListViewModel : ViewModelBase
    {

        private const string url = "http://localhost:54100/api/EstablishmentEvents";

        public ObservableCollection<EstablishmentEventViewModel> Events { get { return this._events; } }
        private ObservableCollection<EstablishmentEventViewModel> _events;
        private ObservableCollection<EstablishmentEventViewModel> _filteredEvents;
        public ObservableCollection<EstablishmentEventViewModel> FilteredEvents{ get { return this._filteredEvents; } set { this._filteredEvents = value; } }
        private string _lookupString = null;
        public string LookupString {
            get { return this._lookupString; }
            set {
                this._lookupString = value;
                if (this._lookupString != null)
                {
                    FilteredEvents = new ObservableCollection<EstablishmentEventViewModel>(Events.Where(pro => pro.Name.ToLower().Contains(this._lookupString)));
                }
                else
                {
                    FilteredEvents = Events;
                }
            }
        }

        public EstablishmentEventListViewModel()
        {
            _events = new ObservableCollection<EstablishmentEventViewModel>();
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
            var events = JsonConvert.DeserializeObject<EstablishmentEvent[]>(json, new EstablishmentEventJsonConverter());
            Events.Clear();
            foreach (EstablishmentEvent eventje in events)
            {
                EstablishmentEventViewModel evm = new EstablishmentEventViewModel();
                evm.Event = eventje;
                Events.Add(evm);
            }
        }
    }
}
