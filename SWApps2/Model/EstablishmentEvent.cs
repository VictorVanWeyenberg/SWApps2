using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class represents Establishment events
    /// </summary>
    public class EstablishmentEvent : Event
    {
        /// <summary>
        /// Sole constructor
        /// </summary>
        /// <param name="establishment">The establishment that corresponds to this event</param>
        /// <param name="name">The event's name</param>
        /// <param name="description">A description for this event</param>
        /// <param name="start">The start date for this event</param>
        /// <param name="end">The end date for this event</param>
        public EstablishmentEvent(Establishment establishment, string name, string description, DateTime start, DateTime end)
        : base(establishment, name, description, start, end)
        {
        }
    }
}
