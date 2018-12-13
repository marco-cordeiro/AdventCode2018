using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode2018.Day8
{
    public class LicenseManager
    {
        private Node _licenseNode;

        public void Load(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(stream);
            }
        }

        public void Load(Stream stream)
        {
            var data = LoadData(stream).GetEnumerator();

            _licenseNode = ReadNode(data);
        }

        public int CalculateChecksum()
        {
            return _licenseNode.CalculateChecksum();
        }

        private Node ReadNode(IEnumerator<int> data)
        {
            data.MoveNext();
            var numberOfNodes = data.Current;
            data.MoveNext();
            var numberOfEntries = data.Current;

            var nodes = ReadNodes(data, numberOfNodes);
            var entries = ReadEntries(data, numberOfEntries);

            return new Node(nodes.ToArray(), entries.ToArray());
        }

        private IEnumerable<int> ReadEntries(IEnumerator<int> data, int numberOfEntries)
        {
            for (var i = 0; i < numberOfEntries; i++)
            {
                data.MoveNext();
                yield return data.Current;
            }
        }

        private IEnumerable<Node> ReadNodes(IEnumerator<int> data, int numberOfNodes)
        {
            for (var i = 0; i < numberOfNodes; i++)
            {
                yield return ReadNode(data);
            }
        }

        public int Checksum()
        {
            return _licenseNode.Checksum;
        }

        private IEnumerable<int> LoadData(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var str = new char[3];
                var pos = 0;
                while (!reader.EndOfStream)
                {
                    var @char = reader.Read();
                    str[pos++] = (char)@char;

                    if (@char != 32 && !reader.EndOfStream) continue;

                    yield return int.Parse(new string(str));
                    str = new char[3];
                    pos = 0;
                }
            }
        }

        private class Node
        {
            public Node(Node[] nodes, int[] entries)
            {
                Nodes = nodes;
                Entries = entries;
                Checksum = nodes.Sum(x => x.Checksum) + entries.Sum();
            }

            public Node[] Nodes { get; }
            public int[] Entries { get; }
            public int Checksum { get; }

            public int CalculateChecksum( )
            {
                if (Nodes.Length == 0)
                    return Checksum;

                var result = 0;
                foreach (var entry in Entries)
                {
                    if (entry <= Nodes.Length)
                        result += Nodes[entry-1].CalculateChecksum();
                }
                return result;
            }
        }
    }
}