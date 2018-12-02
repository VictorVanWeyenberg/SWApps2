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

namespace SWApps2.ViewModel
{
    public class EstablishmentListViewModel : ViewModelBase
    {
        public ObservableCollection<EstablishmentViewModel> Establishments { get { return _establishments; } set { _establishments = value; } }
        private ObservableCollection<EstablishmentViewModel> _establishments;
        public EstablishmentListViewModel()
        {
            _establishments = new ObservableCollection<EstablishmentViewModel>();
            FakeDataService fakenews = new FakeDataService();
            foreach (Establishment establishment in fakenews.Establishments)
            {
                var evm = new EstablishmentViewModel();
                evm.Establishment = establishment;
                _establishments.Add(evm);
            }
        }
    }
}
