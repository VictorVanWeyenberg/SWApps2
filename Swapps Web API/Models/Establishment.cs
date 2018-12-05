using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class Establishment
    {
        [Key]
        public int EstablishmentID { get; set; }
        [NotMapped]
        public ICollection<string> Tags { get; set; }
        public string TagsString { get { return string.Join(",", Tags); } set { Tags = value.Split(',').ToList(); } }

        public string Name { get; set; }
        //Image is ignored, currently a locally stored image is used
        [NotMapped]
        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        public Address Address { get; set; }


        public ICollection<TimeInterval> ServiceHours { get; set; }

        public ICollection<Promotion> Promotions { get; set; }

        public ICollection<EstablishmentEvent> Events { get; set; }

        [NotMapped]
        public EstablishmentType Type { get; set; }
        //We map the Type to string for use in DB
        public string EstablishmentTypeString { get { return Type.ToString(); } set { Type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), value); } }

    }
}