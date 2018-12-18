using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using SWApps2.Converters;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class EstablishmentEventViewModel : ViewModelBase
    {
        private const string GETEVENTBYID = "http://localhost:54100/api/EstablishmentEvents/";
        #region Attributes

        private EstablishmentEvent _event;
        public EstablishmentEvent Event { get { return this._event; } set 
                {
                this._event = value;
                ParseInterval();
            } }
        #endregion

        #region Properties
        public string Name { get { return Event.Name; } }
        public string Interval { get; private set; }
        public string Description { get { return Event.Description; } }
        public string EstablishmentName { get { return Event.Establishment.Name; } }
        #endregion
        public EstablishmentEventViewModel() {
            // Messenger.Default.Register<IDArgs>(this, LoadData);
        }

        private async void LoadData(IDArgs id)
        {
            if (id?.ID != null)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string jsonresult = await client.GetStringAsync(new Uri(GETEVENTBYID + id.ID));
                DownloadCompleted(jsonresult);
            }
        }

        private void DownloadCompleted(string json)
        {
            var eventje = JsonConvert.DeserializeObject<EstablishmentEvent>(json, new EstablishmentEventJsonConverter());
            this.Event = eventje;
        }

        //Redo this
        public void ParseInterval()
        {
            
            StringBuilder sb = new StringBuilder();
            DateTime beginDate = Event.Start.Date;
            DateTime endDate = Event.End.Date;

            sb.Append(beginDate.DayOfWeek.ToString());
            sb.Append(" ");
            sb.Append(beginDate.ToString("dd MMM yyyy"));
            if (beginDate.CompareTo(endDate) != 0)
            {
                sb.Append(" - ");
                sb.Append(endDate.DayOfWeek.ToString());
                sb.Append(" ");
                sb.Append(endDate.ToString("dd MMM yyyy"));
            }
            sb.Append("\n");

            DateTime beginTime = this.Event.Start;
            DateTime endTime = this.Event.End;

            sb.Append(beginTime.ToString("HH:mm"));
            sb.Append(" - ");
            sb.Append(endTime.ToString("HH:mm"));
            this.Interval = sb.ToString();
        }
    }
}
