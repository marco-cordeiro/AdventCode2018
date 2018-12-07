using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2018.Day4
{
    public static class EventLoader
    {
        private const string RegularExpression = @"\[(?<time>.*)\] (?:(?<begin>Guard #(?<guard>\d*) begins shift)|(?<sleeps>falls asleep)|(?<wakes>wakes up))";

        public static IEnumerable<IShiftEvent> Load(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    yield return ParseEvent(line);
                }
            }
        }

        public static IEnumerable<IShiftEvent> Load(string[] lines)
        {
            return lines.Select(ParseEvent);
        }

        private static IShiftEvent ParseEvent(string line)
        {
            var regex = new Regex(RegularExpression);

            var matches = regex.Match(line);

            var time = DateTime.Parse(matches.Groups["time"].Value);

            if (matches.Groups["begin"].Success)
            {
                var guard = int.Parse(matches.Groups["guard"].Value);
                return new BeginShiftEvent(time, guard);
            }

            if (matches.Groups["sleeps"].Success)
            {
                return new FallsAsleepEvent(time);
            }

            if (matches.Groups["wakes"].Success)
            {
                return new WakesUpEvent(time);
            }

            return null;
        }
    }
}