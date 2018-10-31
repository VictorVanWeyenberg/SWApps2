using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class Entrepreneur : User
    {
        //the establishment of this entrepreneur
        private Establishment _establishment;

        public Entrepreneur(string firstname, string lastname, string email, Establishment establishment)
            : base(firstname, lastname, email)
        {
            Establishment = establishment;
        }

        public Establishment Establishment
        {
            ///<summary>
            ///get and set Establishment property
            ///Setter uses <see cref="ObservableObject.Set{T}(string, ref T, T)"/>
            /// </summary>
            get { return _establishment; }
            set { Set("Establishment", ref _establishment, value); }
        }
    }
}
