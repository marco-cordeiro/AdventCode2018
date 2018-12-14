using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2018.Day7
{
    public class InstructionMagician
    {
        private readonly int _stepBaseCost;
        private const string RegularExpression = @"Step (?<step1>[A-Z]) must be finished before step (?<step2>[A-Z]) can begin.";
        private static Regex _regex = new Regex(RegularExpression);
        private Dictionary<char, List<char>> _instructions;

        public InstructionMagician(int stepBaseCost = 0)
        {
            _stepBaseCost = stepBaseCost;
        }

        public void Load(string filename)
        {
            var steps = new Dictionary<char, List<char>>();

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var dependency = ParseEvent(line);

                    if (!steps.TryGetValue(dependency.Item2, out var dependencies))
                        steps.Add(dependency.Item2, dependencies = new List<char>());

                    dependencies.Add(dependency.Item1);

                    if (!steps.TryGetValue(dependency.Item1, out dependencies))
                        steps.Add(dependency.Item1, new List<char>());
                }
            }

            _instructions = steps;
        }

        public void Load(string[] lines)
        {
            var steps = new Dictionary<char,List<char>>();
            foreach (var dependency in lines.Select(ParseEvent))
            {
                if (!steps.TryGetValue(dependency.Item2, out var dependencies))
                    steps.Add(dependency.Item2, dependencies = new List<char>());

                dependencies.Add(dependency.Item1);

                if (!steps.TryGetValue(dependency.Item1, out dependencies))
                    steps.Add(dependency.Item1, new List<char>());
            }

            _instructions = steps;
        }

        public string SortInstructions()
        {
            var result = new List<char>();
            var steps = new Dictionary<char, List<char>>(_instructions);

            while (steps.Count > 0)
            {
                var nextStep = steps.Where(x => x.Value.Count == 0 || x.Value.All(i => result.Contains(i)))
                    .Select(x => x.Key).OrderBy(x => x).First();

                result.Add(nextStep);
                steps.Remove(nextStep);
            }

            return string.Join(string.Empty, result);
        }

        public int CalculateConstructionTime(int workers)
        {
            var time = 0;

            var result = new List<(char,int)>();
            var steps = new Dictionary<char, List<char>>(_instructions);
            
            while (steps.Count > 0)
            {
                var availableWorkers = workers - result.Count(x => time < x.Item2);
                var t = time++;
                if (availableWorkers == 0) continue;

                var availableStep = steps.Where(x => x.Value.Count == 0 || x.Value.All(i => result.Any(d => d.Item1 == i && t >= d.Item2)))
                    .Select(x => x.Key).OrderBy(x => x).Take(availableWorkers);

                foreach (var nextStep in availableStep)
                {
                    result.Add((nextStep, t + _stepBaseCost + (int)nextStep - 64));
                    steps.Remove(nextStep);
                }
            }

            var sortedInstructions = string.Join(string.Empty, result.OrderBy(x => x.Item2).ThenBy(x => x.Item1).Select(x => x.Item1));

            return result.Max(x => x.Item2);
        }

        private static (char, char) ParseEvent(string line)
        {
            var matches = _regex.Match(line);
            return (matches.Groups["step1"].Value[0], matches.Groups["step2"].Value[0]);
        }
    }
}