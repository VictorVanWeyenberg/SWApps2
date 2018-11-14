using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWApps2.Model;

namespace SWApps2.ViewModel
{
    class EstablishmentViewModel
    {
        private Establishment _establishment;
        public EstablishmentViewModel(Establishment establishment)
        {
            this._establishment = establishment;
        }

        public String Name { get { return this._establishment.Name; } }
        public Address Address { get { return this._establishment.Address; } }
        public Uri Image { get { return this._establishment.Image; } }
    }
}
