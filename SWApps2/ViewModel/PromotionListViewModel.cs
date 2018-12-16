using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using SWApps2.Converters;
using SWApps2.Data;
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
    public class PromotionListViewModel : ViewModelBase
    {
        private const string url = "http://localhost:54100/api/Promotions";
        public ObservableCollection<PromotionViewModel> Promotions { get; set; }
        private ObservableCollection<PromotionViewModel> _filteredPromotions;
        public ObservableCollection<PromotionViewModel> FilteredPromotions { get { return this._filteredPromotions; } set { this._filteredPromotions = value; } }
        private string _lookupString = null;
        public string LookupString { get { return this._lookupString; } set {
                this._lookupString = value;
                if (this._lookupString != null)
                {
                    FilteredPromotions = new ObservableCollection<PromotionViewModel>(Promotions.Where(pro => pro.Name.ToLower().Contains(this._lookupString)));
                }
                else
                {
                    FilteredPromotions = Promotions;
                }
            } }

        public PromotionListViewModel()
        {
            Promotions = new ObservableCollection<PromotionViewModel>();
            _filteredPromotions = Promotions;
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
            var promotions = JsonConvert.DeserializeObject<Promotion[]>(json, new PromotionJsonConverter());
            Promotions.Clear();
            foreach (Promotion promotion in promotions)
            {
                PromotionViewModel evm = new PromotionViewModel();
                evm.Promotion = promotion;
                Promotions.Add(evm);
            }
        }
    }
}
