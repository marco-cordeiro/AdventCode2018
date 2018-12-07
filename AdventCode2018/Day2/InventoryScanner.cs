using System;
using System.Linq;

namespace AdventCode2018.Day2
{
    public class InventoryScanner
    {
        public int ScanBox(string[] ids)
        {
            var doubles = 0;
            var triples = 0;

            foreach (var id in ids)
            {
                var (d, t) = ScanId(id);
                doubles += d;
                triples += t;
            }

            return doubles * triples;
        }

        private (int, int) ScanId(string id)
        {
            //97-122

            var occurrences = new byte[26];

            foreach (var @char in id)
            {
                occurrences[@char - 97]++;
            }

            //need to check the last result
            var doubles = occurrences.Count(x=>x==2);
            var triples = occurrences.Count(x=>x==3);

            return (Math.Min(doubles, 1), Math.Min(triples, 1));
        }
    }
}