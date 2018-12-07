using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCode2018.Day2
{
    public class InventoryScanner
    {
        public int Checksum(string[] ids)
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

        public string[] FindPotentialMatches(string[] ids)
        {
            var matches = new List<string>();

            for (int i = 0; i < ids.Length - 1; i++)
            {
                for (int k = i + 1; k < ids.Length - 1; k++)
                {
                    if (CheckForMatch(ids[i], ids[k], out var match))
                    {
                        matches.Add(match);
                    }
                }
            }

            return matches.ToArray();
        }

        private bool CheckForMatch(string id1, string id2, out string match)
        {
            var str = new StringBuilder();
            match = null;
            if (id1.Length != id2.Length) return false;

            var differs = 0;

            for (int i = 0; i < id1.Length; i++)
            {
                if (id1[i] != id2[i])
                {
                    differs++;

                    if (differs > 1) break;

                    continue;
                }

                str.Append(id1[i]);
            }

            if (differs > 1)
                return false;

            match = str.ToString();
            return true;
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