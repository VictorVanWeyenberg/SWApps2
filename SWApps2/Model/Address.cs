using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// An address as used by <see cref="Establishment"/>
    /// </summary>
    public class Address
    {
        //Constants
        private const string country = "België";
        private const string city = "Gent";

        /// <summary>
        /// Sole constructor
        /// </summary>
        /// <param name="street">The street portion of the address</param>
        /// <param name="number">The number portion of the address</param>
        public Address(string street, int number)
        {
            Street = street;
            Number = number;
        }

        /// <summary>
        /// This method will convert the address to a string
        /// </summary>
        /// <returns>A string representation of this address</returns>
        public override string ToString()
        {
            return Street + " " + Number + ", " + city;
        }

        #region Properties
        public string City
        {
            get { return city; }
        }
        public string Country
        {
            get { return country; }
        }

        public string Street { get; set; }

        public int Number { get; set; }

        #endregion
    }
}
