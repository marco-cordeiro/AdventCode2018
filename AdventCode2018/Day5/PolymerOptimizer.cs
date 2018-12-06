using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCode2018.Day5
{
    public class PolymerOptimizer
    {
        public string Optimize(string polymer)
        {
            if (string.IsNullOrWhiteSpace(polymer)) return string.Empty;

            var data = polymer.Select(c => c).Where(c => c.IsValidIsotope()).ToList();

            Optimize(data);

            var result = new StringBuilder();

            foreach (var @char in data)
            {
                result.Append(@char);
            }

            return result.ToString();
        }

        public void Optimize(List<char> data)
        {
            while (true)
            {
                var explode = new List<int>();
                var skip = false;

                for (var i = 1; i < data.Count; i++)
                {
                    if (skip)
                    {
                        skip = false;
                        continue;
                    }

                    var previous = data[i - 1];
                    var current = data[i];

                    if (!previous.ReactsWith(current)) continue;

                    explode.Add(i - 1);
                    explode.Add(i);
                    skip = true;
                }

                if (explode.Count == 0) break;

                foreach (var pos in explode.OrderByDescending(x => x))
                {
                    data.RemoveAt(pos);
                }
            }
        }
    }
}