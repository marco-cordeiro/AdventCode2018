using System;

namespace AdventCode2018.Day4
{
    public interface IShiftEvent
    {
        DateTime Time { get; }
    }

    public abstract class ShiftEvent : IShiftEvent
    {
        protected ShiftEvent(DateTime time)
        {
            Time = time;
        }

        public DateTime Time { get; }
    }

    public class BeginShiftEvent : ShiftEvent
    {
        public BeginShiftEvent(DateTime time, int guard) : base(time)
        {
            Guard = guard;
        }

        public int Guard { get; }
    }

    public class FallsAsleepEvent : ShiftEvent
    {
        public FallsAsleepEvent(DateTime time) : base(time)
        {
        }
    }

    public class WakesUpEvent : ShiftEvent
    {
        public WakesUpEvent(DateTime time) : base(time)
        {
        }
    }
}