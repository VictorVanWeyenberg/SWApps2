using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    /// <summary>
    /// This class is used as wrapper class for EF to map Tags to an <see cref="ICollection{T}"/> for use in <see cref="Establishment"/>
    /// </summary>
    public class Tag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Value { get; set; }
    }
}