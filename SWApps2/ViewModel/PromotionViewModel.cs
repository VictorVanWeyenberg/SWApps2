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
    public class PromotionViewModel
    {
        private const string GETPROMOTIONBYID = "http://localhost:54100/api/Promotions/";
        public Promotion _promotion;
        public Promotion Promotion { get { return this._promotion; } set 
                {
                this._promotion = value;
                ParseInterval();
            }  }
        public PromotionViewModel()
        {
            // Messenger.Default.Register<IDArgs>(this, LoadData);
        }

        private async void LoadData(IDArgs id)
        {
            if (id?.ID != null)
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string jsonresult = await client.GetStringAsync(new Uri(GETPROMOTIONBYID + id.ID));
                DownloadCompleted(jsonresult);
            }
        }

        private void DownloadCompleted(string json)
        {
            var promotion = JsonConvert.DeserializeObject<Promotion>(json, new PromotionJsonConverter());
            this.Promotion = promotion;
        }
        public Establishment Establishment { get { return this._promotion.Establishment; } }
        public string Name { get { return this._promotion.Name; } }
        public string Description { get { return this._promotion.Description; } }
        public string Date { get { return this._promotion.Start.ToString("dd/MM/yyyy"); } }
        public string Time {
            get {
                return new StringBuilder("From ").Append(_promotion.Start.ToString("HH:mm")).Append(" to ").Append(_promotion.End.ToString("HH:mm")).ToString();
            }
        }
        public string Interval { get; set; }
        public string EstablishmentName { get { return this._promotion.Establishment.Name; } }
        public void ParseInterval()
        {
            StringBuilder sb = new StringBuilder();
            DateTime beginDate = this._promotion.Start.Date;
            DateTime endDate = this._promotion.End.Date;

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

            DateTime beginTime = this._promotion.Start;
            DateTime endTime = this._promotion.End;

            sb.Append(beginTime.ToString("HH:mm"));
            sb.Append(" - ");
            sb.Append(endTime.ToString("HH:mm"));
            this.Interval = sb.ToString();
        }
    }
}
