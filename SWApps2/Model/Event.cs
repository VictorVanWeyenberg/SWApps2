using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// An Event describes some action that happens at a set moment in time, for a certain <see cref="Establishment"/>
    /// </summary>
    public class Event
    {
        #region Properties

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Establishment Establishment { get; set; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="description">The description of the event</param>
        /// <param name="start">The start date</param>
        /// <param name="end">The end date</param>
        /// <param name="establishment">The establishment that is linked to this event</param>
        public Event(Establishment establishment, string name, string description, DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            Name = name;
            Description = description;
            Establishment = establishment;
        }
    }
}
