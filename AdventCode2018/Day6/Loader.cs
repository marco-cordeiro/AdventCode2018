using System;
using System.Collections.Generic;
using System.IO;

namespace AdventCode2018.Day6
{
    public static class Loader
    {
        public static IEnumerable<Coordinate> Load(string[] lines)
        {
            var id = (short)0;
            foreach (var line in lines)
            {
                var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                yield return new Coordinate(id++, short.Parse(parts[0].Trim()), short.Parse(parts[1].Trim()));
            }
        }

        public static IEnumerable<Coordinate> Load(string filename)
        {
            var id = (short)0;
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    yield return new Coordinate(id++, short.Parse(parts[0].Trim()), short.Parse(parts[1].Trim()));
                }
            }
        }
    }
}