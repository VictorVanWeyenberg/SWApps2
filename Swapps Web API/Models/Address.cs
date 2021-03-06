﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }

        public int Number { get; set; }

        [Required]
        public string Street { get; set; }
    }
}