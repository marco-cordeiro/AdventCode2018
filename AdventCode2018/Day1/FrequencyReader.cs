using System.Collections.Generic;

namespace AdventCode2018.Day1
{
    public class FrequencyReader : IFrequencyReader
    {
        private readonly int[] _frequencies;

        public FrequencyReader(params int[] args)
        {
            _frequencies = args;
        }

        public IEnumerable<int> Frequencies => _frequencies;
    }
}