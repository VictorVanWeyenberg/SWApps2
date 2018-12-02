using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swapps_Web_API.Models
{
    public class TimeInterval
    {
        [Key]
        public int TimeIntervalID { get; set; }

        [ForeignKey("Establishment")]
        public int? EstablishmentID { get; set; }

        public virtual Establishment Establishment { get; set; }


        public int DayOfWeek { get; set; }

        public int StartHour { get; set; }

        public int StartMinute { get; set; }

        public int EndHour { get; set; }

        public int EndMinute { get; set; }
    }
}