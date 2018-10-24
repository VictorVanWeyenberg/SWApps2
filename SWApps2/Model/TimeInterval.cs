using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class TimeInterval
    {
        ///<summary>
        ///This class represents a time interval as used by <see cref="ServiceHours"/>
        ///</summary>
        /// <remarks>
        /// This class does not extend from <see cref="GalaSoft.MvvmLight.ObservableObject"/>
        /// That functionality is reserved for <see cref="ServiceHours"/>
        /// </remarks>

        //the start time of this time interval
        private LocalTime _startTime;

        //the end time of this time interval
        private LocalTime _endTime;

        //Constructor
        public TimeInterval(LocalTime start, LocalTime end)
        {
            Start = start;
            End = end;
        }

        //Start Property
        public LocalTime Start
        {
            get { return _startTime; }
            set
            {
                //accept DateTime.Now as valid date or not?
                //if start time is not now or in the future, throw exception
                if (value.CompareTo(new LocalTime(DateTime.Now.Hour, DateTime.Now.Minute)) < 0)
                {
                    throw new ArgumentOutOfRangeException("Een start datum voor een tijdsinterval moet in de toekomst liggen");
                }
                _startTime = value;
            }
        }
        //End Propety
        public LocalTime End
        {
            get { return _endTime; }
            set
            {
                //if end time is not later than start time, throw exception
                if (value.CompareTo(_startTime) <=0 )
                {
                    throw new ArgumentOutOfRangeException("Een eind datum voor een tijdsinterval moet later zijn dan de start datum");
                }
                _endTime = value;
            }
        }
    }
}
