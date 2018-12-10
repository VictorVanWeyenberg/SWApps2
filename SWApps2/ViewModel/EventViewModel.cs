using SWApps2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class EventViewModel
    {
        #region Attributes

        private Event _event;
        #endregion

        #region Properties
        public string Name { get { return _event.Name; } }
        public string Interval { get; private set; }
        #endregion
        public EventViewModel(Event evt) {
            _event = evt;
            ParseInterval();
        }

        //Redo this
        public void ParseInterval()
        {
            
            StringBuilder sb = new StringBuilder();
            DateTime beginDate = _event.Start.Date;
            DateTime endDate = _event.End.Date;

            sb.Append(beginDate.DayOfWeek.ToString());
            sb.Append(" ");
            sb.Append(beginDate.ToString("dd MMM yyyy"));
            if (beginDate.CompareTo(endDate) != 0)
            {
                sb.Append(" - ");
                sb.Append(endDate.DayOfWeek.ToString());
                sb.Append(" ");
                sb.Append(endDate.ToString("dd MMM yyyy"));
            }
            sb.Append("\n");

            DateTime beginTime = this._event.Start;
            DateTime endTime = this._event.End;

            sb.Append(beginTime.ToString("HH:mm"));
            sb.Append(" - ");
            sb.Append(endTime.ToString("HH:mm"));
            this.Interval = sb.ToString();
        }
    }
}
