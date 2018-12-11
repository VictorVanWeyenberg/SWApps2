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
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        public ICollection<Tag> Tags { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public ICollection<TimeInterval> ServiceHours { get; set; }

        [Required]
        public ICollection<Promotion> Promotions { get; set; }

        [Required]
        public ICollection<EstablishmentEvent> Events { get; set; }

        [NotMapped]
        public EstablishmentType Type { get; set; }

        //We map the Type to string for use in DB
        public string EstablishmentTypeString { get { return Type.ToString(); } set { Type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), value); } }
    }
}