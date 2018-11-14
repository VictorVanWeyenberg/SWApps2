using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWApps2.Data;
using SWApps2.Model;
using System.Collections.ObjectModel;

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
                _establishments.Add(new EstablishmentViewModel(establishment));
                Console.WriteLine(establishment.Name);
            }
        }
    }
}
