using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day4
{
    public class Tests
    { 
        [Test]
        [TestCase("[1518-03-02 23:58] Guard #3229 begins shift", "[1518-03-03 00:18] falls asleep", "[1518-03-03 00:38] wakes up", ExpectedResult = 20)]
        public int TestAlgorithm(params string[] rawEvents)
        {
            var events = EventLoader.Load(rawEvents).ToArray();

            Assert.That(events, Has.Length.EqualTo(3));
            Assert.That(events, Has.One.InstanceOf<BeginShiftEvent>());
            Assert.That(events, Has.One.InstanceOf<FallsAsleepEvent>());
            Assert.That(events, Has.One.InstanceOf<WakesUpEvent>());

            var result = events.ProcessGuardAsleepTimes();

            return result.FirstOrDefault().Value.SleptMinutes;
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var events = EventLoader.Load("day4/input.txt").OrderBy(x => x.Time);

            var sleepTimes = events.ProcessGuardAsleepTimes();
            sleepTimes.CalculateSleetStats();

            var max = sleepTimes.Max(x => x.Value.SleptMinutes);

            var snooziestGuard = sleepTimes.FirstOrDefault(x => x.Value.SleptMinutes == max).Value;

            Console.WriteLine($"Guard : {snooziestGuard.Id}");
            Console.WriteLine($"Answer : {snooziestGuard.Id * snooziestGuard.MinuteWithMostSnoozes}");
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var events = EventLoader.Load("day4/input.txt").OrderBy(x => x.Time);

            var sleepTimes = events.ProcessGuardAsleepTimes();
            sleepTimes.CalculateSleetStats();

            var max = sleepTimes.Max(x => x.Value.MostSnoozesInTheMinute);

            var snooziestGuard = sleepTimes.FirstOrDefault(x => x.Value.MostSnoozesInTheMinute == max).Value;

            Console.WriteLine($"Guard : {snooziestGuard.Id}");
            Console.WriteLine($"Answer : {snooziestGuard.Id * snooziestGuard.MinuteWithMostSnoozes}");
        }
    }

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