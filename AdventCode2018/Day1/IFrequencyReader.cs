using System.Collections.Generic;

namespace AdventCode2018.Day1
{
    public interface IFrequencyReader
    {
        IEnumerable<int> Frequencies { get; }
    }
}