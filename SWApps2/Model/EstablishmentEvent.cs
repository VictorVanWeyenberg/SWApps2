using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class EstablishmentEvent : Event
    {
        public EstablishmentEvent(string name, string description, DateTime start, DateTime end)
        : base(name, description, start, end)
        {
        }
    }
}
