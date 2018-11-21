using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class Event : ObservableObject
    {

        private Establishment _establishment;
        //the start date of the event
        private DateTime _startDate;
        //the end date of the event
        private DateTime _endDate;

        //The name of this event
        private string _name;
        //The Desription for this event
        private string _description;

        #region Properties
        public Establishment Establishment {
            get { return _establishment; }
            set { _establishment = value; }
        }
        //Start date property
        public DateTime Start
        {
            get { return _startDate; }
            set { Set("Start", ref _startDate, value); }
        }

        //End date property
        public DateTime End
        {
            get { return _endDate; }
            set { Set("End", ref _endDate, value); }
        }

        //Name property
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }

        //Description property
        public string Description
        {
            get { return _description; }
            set { Set("Description", ref _description, value); }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="description">The description of the event</param>
        /// <param name="start">The start date</param>
        /// <param name="end">The end date</param>
        public Event(Establishment establishment, string name, string description, DateTime start, DateTime end)
        {
            Establishment = establishment;
            Start = start;
            End = end;
            Name = name;
            Description = description;
        }
    }
}
