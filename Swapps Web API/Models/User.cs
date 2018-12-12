using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("AbstractUser")]
        public int AbstractUserID { get; set; }

        [Required]
        public AbstractUser AbstractUser { get; set; }

        [Required]
        public ICollection<Establishment> Subscriptions { get; set; }
    }
}