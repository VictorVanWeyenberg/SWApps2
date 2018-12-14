using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// The opening hours for an establishment
    /// </summary>
    public class ServiceHours
    {
        public TimeInterval[] Hours { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceHours">An array containing the opening hours</param>
        public ServiceHours(TimeInterval[] serviceHours)
        {
            Hours = serviceHours;
        }

        public ServiceHours() : this(new TimeInterval[7])
        { }
    }
}
