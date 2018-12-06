using System.Collections.Generic;
using System.IO;

namespace AdventCode2018.Day1
{
    public class FileFrequencyReader : IFrequencyReader
    {
        private readonly string _filename;

        public FileFrequencyReader(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<int> Frequencies => GetFrequencies(_filename);

        private static IEnumerable<int> GetFrequencies(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (int.TryParse(line, out var result))
                        yield return result;
                }
            }
        }
    }
}