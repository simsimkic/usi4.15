using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Model
{
    public class TimeSlot
    {
        public DateTime Start { get; set; }
        public int Duration { get; set; }
        public TimeSlot(DateTime start, int duration)
        {
            Start = start;
            Duration = duration;
        }
        public TimeSlot() { }
        public override string ToString()
        {
            return $"{Start.ToShortDateString()} {Start.TimeOfDay} {Duration}";
        }
    }
}
