using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class ServiceHours : ObservableObject
    {
        //Service Hours of this establishment as Property
        public TimeInterval[] Hours { get; }

        /// <summary>
        /// Set the service hours for a given day
        /// </summary>
        /// <param name="day">the index of the day</param>
        /// <param name="newHours">the new hours, as a <see cref="TimeInterval"/></param>
        public void setHoursForDay(int day, TimeInterval newHours)
        {
            Hours[day] = newHours;
            RaisePropertyChanged("Hours");
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Only allocates an emty array, doesn't fill it
        /// </remarks>
        public ServiceHours()
        {
            Hours = new TimeInterval[7];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 7; i++)
            {
                string dayOfWeek = ((DayOfWeek)i).ToString();
                sb.Append(dayOfWeek);
                sb.Append(":\t");
                if (dayOfWeek.Length < 8) sb.Append("\t");
                sb.Append(Hours[i].ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
