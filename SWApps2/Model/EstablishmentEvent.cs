using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class EstablishmentEvent : Event
    {
        public EstablishmentEvent(Establishment establishment, string name, string description, DateTime start, DateTime end)
        : base(establishment, name, description, start, end)
        {
        }
    }
}
