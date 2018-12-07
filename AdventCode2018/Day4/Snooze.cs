using System;

namespace AdventCode2018.Day4
{
    public struct Snooze
    {
        public Snooze(DateTime started, DateTime stopped, int minutes)
        {
            Started = started;
            Stopped = stopped;
            Minutes = minutes;
        }

        public DateTime Started;
        public DateTime Stopped;

        public int Minutes;
    }
}