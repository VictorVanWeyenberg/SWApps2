using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWApps2.Model

namespace SWApps2.Data
{
    class FakeDataService
    {
        public List<Establishment> Establishments { get; }

        public FakeDataService()
        {
            establishments = new List<Establishment>();
            Address address1 = new Address("Klein Turkije", 8);
            ServiceHours serviceHours1 = new ServiceHours();
            NodaTime.LocalTime openHour = new NodaTime.LocalTime(16, 0);
            NodaTime.LocalTime closeHour = new NodaTime.LocalTime(4, 0);
            TimeInterval timeInterval1 = new TimeInterval(openHour, closeHour);
            for (int i = 0; i < 7; i++)
            {
                serviceHours1.setHoursForDay(i, timeInterval1);
            }
            Establishment establishment1 = new Establishment("Comic Sans", address1, serviceHours1, EstablishmentType.BAR, new Uri("/Assets/comicsans.jpg"));

            establishments.Add(establishment1);
        }
    }
}
