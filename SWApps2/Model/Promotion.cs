using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class represents a promotion
    /// </summary>
    public class Promotion : Event
    {

        public Promotion(Establishment establishment, string name, string description, DateTime start, DateTime end) :
            base(establishment, name, description, start, end)
        {
        }
    }
}
