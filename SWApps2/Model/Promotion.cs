using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class represents a promotion for a certain <see cref="Establishment"/>
    /// </summary>
    public class Promotion : Event
    {
        /// <summary>
        /// Sole constructor
        /// </summary>
        /// <param name="establishment">The establishment linked to this promotion</param>
        /// <param name="name">The name for this promotion</param>
        /// <param name="description">The description for this promotion</param>
        /// <param name="start">The start date for this promotion</param>
        /// <param name="end">The end date for this promotion</param>
        public Promotion(Establishment establishment, string name, string description, DateTime start, DateTime end) :
            base(establishment, name, description, start, end)
        {
        }
    }
}
