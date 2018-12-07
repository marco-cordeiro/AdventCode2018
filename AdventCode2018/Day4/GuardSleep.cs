using System;
using System.Collections.Generic;

namespace AdventCode2018.Day4
{
    public class GuardSleep
    {
        public GuardSleep(int id)
        {
            Snoozes = new List<Snooze>();
            Id = id;
        }

        public int Id { get; }

        public IList<Snooze> Snoozes { get; }

        public int SleptMinutes { get; set; }

        public void AddSnooze(DateTime started, DateTime ended)
        {
            var minutes = ended.Subtract(started).Minutes;
            Snoozes.Add(new Snooze(started, ended, minutes));
            SleptMinutes += minutes;
        }

        public int MinuteWithMostSnoozes { get; set; }

        public int MostSnoozesInTheMinute { get; set; }

    }
}