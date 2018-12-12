using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class Entrepreneur
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        [Required]
        public AbstractUser User { get; set; }

        public int? EstablishmentID { get; set; }

        [ForeignKey("EstablishmentID")]
        public Establishment Establishment { get; set; }

    }
}