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

namespace SWApps2.ViewModel
{
    public class EstablishmentViewModel : ViewModelBase
    {
        private const string CLOSED = "CLOSED";
        private const string GETURL = "http://localhost:54100/api/establishment/owner";

        public EstablishmentViewModel()
        {
            App app = Application.Current as App;
            if (app.User != null && app.User is Entrepreneur)
            {
                Establishment = (app.User as Entrepreneur)?.Establishment;
            }
            else {
                Establishment = app.SelectedEstablishment;
                app.SelectedEstablishment = null;
            }
        }

        private Establishment _establishment;
        public Establishment Establishment
        {
            get { return _establishment; }
            set {
                if (value != null)
                {
                    _establishment = value;
                    Events = new ObservableCollection<EventViewModel>();
                    foreach (EstablishmentEvent establishmentEvent in _establishment.EstablishmentEvents)
                    {
                        Events.Add(new EventViewModel(establishmentEvent));
                    }
                    Promotions = new ObservableCollection<PromotionViewModel>();
                    foreach (Promotion promotion in _establishment.Promotions)
                    {
                        Promotions.Add(new PromotionViewModel(promotion));
                    }
                }
            }
        }

        public string Name { get { return Establishment.Name; } }
        public Address Address { get { return Establishment.Address; } }
        public ServiceHours ServiceHours { get { return Establishment.ServiceHours; } }
        
        public ObservableCollection<EventViewModel> Events { get; set; }
        public ObservableCollection<PromotionViewModel> Promotions { get; set; }

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
                return string.Format("{0}: {1}", dayOfWeek, day?.ToString() ?? CLOSED);
            }
            //Invalid day
            return "";
        }
    }
}
