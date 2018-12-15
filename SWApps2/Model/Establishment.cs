using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// An Establishment is a place/building or similar that is important to application users
    /// </summary>
    public class Establishment
    {
        // An image of the establishment
        private Uri _image;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the establishment</param>
        /// <param name="address">The address of the establishment</param>
        /// <param name="hours">The opening hours of the establishment</param>
        /// <param name="type">The type of establishment</param>
        /// <param name="image">An optional image for the establishment</param>
        public Establishment(string name, Address address, ServiceHours hours, EstablishmentType type, Uri image)
            : this(name, address, hours, type, new List<string>(), new List<Promotion>(), new List<EstablishmentEvent>(), image) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the establishment</param>
        /// <param name="address">The address of the establishment</param>
        /// <param name="hours">The opening hours of the establishment</param>
        /// <param name="type">The type of establishment</param>
        /// <param name="image">An optional image for the establishment</param>
        /// <param name="events">A list of events for this establishment</param>
        /// <param name="promotions">A list of promotions for this establishment</param>
        /// <param name="tags">A list of tags for this establishment</param>
        public Establishment(string name, Address address, ServiceHours hours, EstablishmentType type,
            List<string> tags, List<Promotion> promotions, List<EstablishmentEvent> events, Uri image = null)
        {
            Name = name;
            Address = address;
            ServiceHours = hours;
            Type = type;
            Image = image;
            Tags = tags;
            Promotions = promotions;
            EstablishmentEvents = events;
        }

        #region properties
        public EstablishmentType Type { get; set; }

        /// <summary>
        /// A list of tags that link to this establishment
        /// E.g. "Beer" or "Drinks" for a Bar type Establishment
        /// </summary>
        public List<string> Tags { get; }

        public string Name { get; set; }

        public List<Promotion> Promotions { get; set; }

        public List<EstablishmentEvent> EstablishmentEvents { get; set; }

        public int ID { get; set; }

        public Uri Image
        {
            get { return _image; }
            set
            {
                //if the image isn't specified, use default
                _image = value ?? new Uri("ms-appx:///Assets/comicsans.jpg");
            }
        }

        public Address Address { get; set; }

        public ServiceHours ServiceHours { get; set; }

        #endregion
    }
}
