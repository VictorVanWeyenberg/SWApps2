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
        private long _id;

        public long ID
        {
            get { return _id; }
        }

        public Promotion(string name, string description, DateTime start, DateTime end, long id) :
            base(name, description, start, end)
        {
            _id = id;
        }
    }
}
