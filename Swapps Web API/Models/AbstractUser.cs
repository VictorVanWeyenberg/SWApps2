using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    /// <summary>
    /// AbstractUser is a DB table that holds all application user credentials
    /// </summary>
    public class AbstractUser
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}