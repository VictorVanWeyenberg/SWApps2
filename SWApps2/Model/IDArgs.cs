using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class IDArgs
    {
        private int _id;
        public int ID { get { return this._id; } private set { this._id = value; } }
        public IDArgs(int id)
        {
            this.ID = id;
        }
    }
}
