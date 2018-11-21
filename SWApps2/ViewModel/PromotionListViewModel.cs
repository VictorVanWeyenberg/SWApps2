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
        public ObservableCollection<PromotionViewModel> Promotions { get { return _promotions; } set { _promotions = value; } }
        private ObservableCollection<PromotionViewModel> _promotions;

        public PromotionListViewModel()
        {
            _promotions = new ObservableCollection<PromotionViewModel>();
            FakeDataService fakenews = new FakeDataService();
            foreach (Promotion promotion in fakenews.Promotions)
            {
                _promotions.Add(new PromotionViewModel(promotion));
            }
        }
    }
}
