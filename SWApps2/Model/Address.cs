using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class Address : ObservableObject
    {
        /// <summary>
        /// An address as used by <see cref="Establishment"/>
        /// </summary>
        //Constants
        private const string country = "België";
        private const string city = "Gent";

        //The street part of the address
        private string _street;
        //The number part of the address
        private int _number;

        //Constructor
        public Address(string street, int number)
        {
            Street = street;
            Number = number;
        }

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

        public string Street
        {
            get { return _street; }
            set { Set("Street", ref _street, value); }
        }

        public int Number
        {
            get { return _number; }
            set { Set("Number", ref _number, value); }
        }

        #endregion
    }
}
