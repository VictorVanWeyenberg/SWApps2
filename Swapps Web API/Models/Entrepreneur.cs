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
        public int EntrepreneurID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        [Required]
        public User User { get; set; }

        public int EstablishmentID { get; set; }

        [ForeignKey("EstablishmentID")]
        [Required]
        public Establishment Establishment { get; set; }

    }
}