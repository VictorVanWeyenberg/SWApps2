using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class Establishment : ObservableObject
    {
        //the list of tags for the establishment
        private List<string> _tags;

        //the list of promotions for the establishment
        private List<Promotion> _promotions;

        //the list of events for the establishment
        private List<EstablishmentEvent> _events;

        //the name of this establishment
        private string _name;

        //the type of this establishment
        private EstablishmentType _type;

        // An image of the establishment
        private Uri _image;

        public Uri Image { get { return this._image; } }
        //the address of the establishment
        public Address Address { get; }

        //ServiceHours property
        public ServiceHours Hours { get; }

        public Establishment(string name, Address address, ServiceHours hours, EstablishmentType type, Uri image)
        {
            Name = name;
            Address = address;
            Hours = hours;
            Type = type;
            _tags = new List<string>();
            _promotions = new List<Promotion>();
            _events = new List<EstablishmentEvent>();
            _image = image
        }


        //Type property
        public EstablishmentType Type
        {
            get { return _type; }
            set { Set("Type", ref _type, value); }
        }

        /// <summary>
        /// A readonly version of the list of tags, as a Property
        /// </summary>
        public IEnumerable<string> Tags
        {
            //Get readonly collection of tags
            get { return _tags.AsEnumerable(); }
        }
        //Remove a tag, then raise propertyChanged
        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
            RaisePropertyChanged("Tags");
        }
        //Add a tag, then raise propertyChanged
        public void AddTag(string tag)
        {
            _tags.Add(tag);
            RaisePropertyChanged("Tags");
        }

        //Name Property
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }

        //Readonly list of promotions
        public IEnumerable<Promotion> Promotions
        {
            get { return _promotions.AsEnumerable(); }
        }

        //Remove a Promotion, then raise propertyChanged
        public void RemovePromotion(Promotion promo)
        {
            _promotions.Remove(promo);
            RaisePropertyChanged("Promotions");
        }
        //Add a Promotion, then raise propertyChanged
        public void AddTag(Promotion promo)
        {
            _promotions.Add(promo);
            RaisePropertyChanged("Prmotions");
        }



        /// <summary>
        /// A readonly version of the list of EstablishmentEvent, as a Property
        /// </summary>
        public IEnumerable<EstablishmentEvent> EstablishmentEvents
        {
            //Get readonly collection of tags
            get { return _events.AsEnumerable(); }
        }
        //Remove an event, then raise propertyChanged
        public void RemoveEvent(EstablishmentEvent ev)
        {
            _events.Remove(ev);
            RaisePropertyChanged("EstablishmentEvents");
        }
        //Add an event, then raise propertyChanged
        public void AddTag(EstablishmentEvent ev)
        {
            _events.Add(ev);
            RaisePropertyChanged("EstablishmentEvents");
        }
    }
}
