using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class EstablishmentEvent
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Establishment")]
        public int EstablishmentID { get; set; }

        [Required]
        public Establishment Establishment { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}