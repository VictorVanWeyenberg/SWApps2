using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SWApps2.Model;

namespace SWApps2.ViewModel
{
    public class EstablishmentViewModel : ViewModelBase
    {
        private Establishment _establishment;
        public Establishment Establishment
        {
            get { return _establishment; }
            set {
                _establishment = value;
                Events = new ObservableCollection<EventViewModel>();
                foreach (EstablishmentEvent establishmentEvent in value.EstablishmentEvents)
                {
                    Events.Add(new EventViewModel(establishmentEvent));
                }
                Promotions = new ObservableCollection<PromotionViewModel>();
                foreach (Promotion promotion in value.Promotions)
                {
                    Promotions.Add(new PromotionViewModel(promotion));
                }
            }
        }

        public EstablishmentViewModel() { }

        public string Name { get { return this.Establishment.Name; } }
        public Address Address { get { return this.Establishment.Address; } }
        public ServiceHours ServiceHours { get { return this.Establishment.Hours; } }
        
        public ObservableCollection<EventViewModel> Events { get; set; }
        public ObservableCollection<PromotionViewModel> Promotions { get; set; }

        public static implicit operator EstablishmentViewModel(EstablishmentListViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
