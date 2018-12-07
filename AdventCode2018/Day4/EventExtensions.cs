using System;
using System.Collections.Generic;

namespace AdventCode2018.Day4
{
    public static class EventExtensions
    {
        public static void CalculateSleetStats(this IDictionary<int, GuardSleep> sleepTimes)
        {
            foreach (var guardSleepTimes in sleepTimes.Values)
            {
                var timeMap = new short[60];

                foreach (var snooze in guardSleepTimes.Snoozes)
                {
                    var startedAtMinute = snooze.Started.Minute;
                    var stoppedAtMinute = snooze.Stopped.Minute;

                    for (int i = startedAtMinute; i < stoppedAtMinute; i++)
                    {
                        timeMap[i]++;
                    }
                }

                var minuteWithMostSnooze = 0;
                var mostSnoozeInTheMinute = 0;

                for (int i = 0; i < 60; i++)
                {
                    if (timeMap[i] > mostSnoozeInTheMinute)
                    {
                        minuteWithMostSnooze = i;
                        mostSnoozeInTheMinute = timeMap[i];
                    }
                }

                guardSleepTimes.MinuteWithMostSnoozes = minuteWithMostSnooze;
                guardSleepTimes.MostSnoozesInTheMinute = mostSnoozeInTheMinute;


                Console.WriteLine($"Guard : {guardSleepTimes.Id} Snoozes : {guardSleepTimes.MostSnoozesInTheMinute}");
            }
        }

        public static IDictionary<int, GuardSleep> ProcessGuardAsleepTimes(this IEnumerable<IShiftEvent> events)
        {
            IDictionary<int, GuardSleep> sleepTimes = new Dictionary<int, GuardSleep>();

            var currentGuard = 0;
            var fellAsleep = DateTime.MinValue;

            foreach (var @event in events)
            {
                switch (@event)
                {
                    case BeginShiftEvent e:
                    {
                        currentGuard = e.Guard;
                        if (!sleepTimes.ContainsKey(currentGuard))
                            sleepTimes[currentGuard] = new GuardSleep(currentGuard);

                        continue;
                    }
                    case FallsAsleepEvent _:
                        fellAsleep = @event.Time;
                        continue;
                    case WakesUpEvent _:
                        sleepTimes[currentGuard].AddSnooze(fellAsleep, @event.Time);
                        continue;
                }
            }

            return sleepTimes;
        }
    }
}