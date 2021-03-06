﻿using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime.Text;
using System.Globalization;

namespace SWApps2.Model
{
    /// <summary>
    /// Represents a time interval as used by <see cref="ServiceHours"/>
    /// E.g. 12:00 - 14:00
    /// </summary>
    public class TimeInterval
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start">The start time for this interval</param>
        /// <param name="end">The end time for this interval</param>
        public TimeInterval(LocalTime start, LocalTime end)
        {
            Start = start;
            End = end;
        }

        public LocalTime Start { get; }

        public LocalTime End { get; }

        public override bool Equals(object obj)
        {
            var interval = obj as TimeInterval;
            return interval != null &&
                   Start.Equals(interval.Start) &&
                   End.Equals(interval.End);
        }

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + EqualityComparer<LocalTime>.Default.GetHashCode(Start);
            hashCode = hashCode * -1521134295 + EqualityComparer<LocalTime>.Default.GetHashCode(End);
            return hashCode;
        }

        /// <summary>
        /// Converts this object to a string
        /// </summary>
        /// <returns>A string representation of this object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            CultureInfo culture = (CultureInfo) CultureInfo.InvariantCulture.Clone();
            LocalTimePattern pattern = LocalTimePattern.Create("HH:mm", culture);

            sb.Append(pattern.Format(Start));
            sb.Append(" - ");
            sb.Append(pattern.Format(End));
            return sb.ToString();
        }
    }
}
