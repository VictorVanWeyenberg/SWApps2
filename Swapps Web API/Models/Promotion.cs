using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class Promotion
    {
        [Key]
        public int PromotionID { get; set; }

        [ForeignKey("Establishment")]
        public int? EstablishmentID { get; set; }

        public virtual Establishment Establishment { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}