using System;
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
}