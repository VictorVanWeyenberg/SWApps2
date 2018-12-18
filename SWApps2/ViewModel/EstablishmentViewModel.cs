using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Net.Http;
using SWApps2.Model;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using SWApps2.Converters;

namespace SWApps2.ViewModel
{
    public class EstablishmentViewModel : ViewModelBase
    {
        private const string CLOSED = "CLOSED";
        private const string GETURL = "http://localhost:54100/api/establishment/owner";
        private const string GETESTABLISHMENYBYID = "http://localhost:54100/api/Establishments/";
        private const string SUBSCRIBE = "http://localhost:54100/api/updateUser";

        private Establishment _establishment;
        public Establishment Establishment
        {
            get { return _establishment; }
            set {
                if (value != null)
                {
                    _establishment = value;
                    Events = new ObservableCollection<EstablishmentEventViewModel>();
                    foreach (EstablishmentEvent establishmentEvent in _establishment.EstablishmentEvents)
                    {
                        EstablishmentEventViewModel eventje = new EstablishmentEventViewModel();
                        eventje.Event = establishmentEvent;
                        Events.Add(eventje);
                    }
                    Promotions = new ObservableCollection<PromotionViewModel>();
                    foreach (Promotion promotion in _establishment.Promotions)
                    {
                        PromotionViewModel pvm = new PromotionViewModel();
                        pvm.Promotion = promotion;
                        Promotions.Add(pvm);
                    }
                }
            }
        }

        public EstablishmentViewModel() {
            // Messenger.Default.Register<IDArgs>(this, LoadData);
        }

        private async void LoadData(IDArgs id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string jsonresult = await client.GetStringAsync(new Uri(GETESTABLISHMENYBYID + id.ID));
            DownloadCompleted(jsonresult);
            Callback();
        }

        private void DownloadCompleted(string json)
        {
            var establishment = JsonConvert.DeserializeObject<Establishment>(json, new EstablishmentJsonConverter());
            foreach (EstablishmentEvent eventje in establishment.EstablishmentEvents)
            {
                eventje.Establishment = establishment;
            }
            this.Establishment = establishment;
        }

        public string Name { get { return Establishment.Name; } }
        public Address Address { get { return Establishment.Address; } }
        public ServiceHours ServiceHours { get { return Establishment.ServiceHours; } }
        
        public ObservableCollection<EstablishmentEventViewModel> Events { get; set; }
        public ObservableCollection<PromotionViewModel> Promotions { get; set; }
        public Action Callback { get; internal set; }

        public string HoursForDayToString(int number)
        {
            if (number >= 0 && number < 7)
            {
                //Get the day as string
                string dayOfWeek = ((DayOfWeek)number).ToString();
                //Get the hours
                TimeInterval day = _establishment.ServiceHours.Hours[number];
                //If there is an object -> hours available
                //Else they are closed on said day
                return string.Format("{0}: {1}", dayOfWeek, day.ToString() ?? CLOSED);
            }
            //Invalid day
            return "";
        }

        public void SubscribeToEstablishment()
        {
            User user = (Application.Current as App).User as User;
            user.SubsribedEstablishments.Add(this.Establishment);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            StringContent content = new StringContent(JsonConvert.SerializeObject(user, new UserJsonConverter()), Encoding.UTF8, "application/json");
            string jsonResult = client.PostAsync(SUBSCRIBE, content).Result.Content.ToString();
        }
    }
}
