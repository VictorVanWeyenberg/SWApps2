﻿using GalaSoft.MvvmLight;
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

namespace SWApps2.ViewModel
{
    public class EstablishmentListViewModel : ViewModelBase
    {
        const string url = "http://localhost:54100/api/establishment";
        public ObservableCollection<EstablishmentViewModel> Establishments { get { return _establishments; } set { _establishments = value; } }
        private ObservableCollection<EstablishmentViewModel> _establishments;
        public EstablishmentListViewModel()
        {
            _establishments = new ObservableCollection<EstablishmentViewModel>();
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
            var establishments = JsonConvert.DeserializeObject<Establishment[]>(json, new EstablishmentJsonConverter2());
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
