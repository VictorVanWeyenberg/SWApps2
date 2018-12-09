using GalaSoft.MvvmLight;
using SWApps2.Data;
using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class PromotionListViewModel : ViewModelBase
    {
        public ObservableCollection<PromotionViewModel> Promotions { get; set; }

        public PromotionListViewModel()
        {
            Promotions = new ObservableCollection<PromotionViewModel>();
            //Get data
            FakeDataService fakenews = new FakeDataService();
            foreach (Promotion promotion in fakenews.Promotions)
            {
                Promotions.Add(new PromotionViewModel(promotion));
            }
        }
    }
}
