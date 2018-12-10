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
        private const string CLOSED = "CLOSED";

        public TimeInterval[] Hours { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceHours">An array containing the opening hours</param>
        public ServiceHours(TimeInterval[] serviceHours)
        {
            Hours = serviceHours;
        }

        /// <summary>
        /// Returns a string representation of the opening hours for a certain day
        /// </summary>
        /// <param name="number">The day of the week as an integer in the range [0-6]</param>
        /// <returns></returns>
        public string HoursForDayToString(int number)
        {
            if (number >= 0 && number < 7)
            {
                //Get the day as string
                string dayOfWeek = ((DayOfWeek)number).ToString();
                //Get the hours
                TimeInterval day = Hours[number];
                //If there is an object -> hours available
                //Else they are closed on said day
                return string.Format("{0}: {1}", dayOfWeek, day.ToString() ?? CLOSED);         
            }
            //Invalid day
            return "";
        }
    }
}
