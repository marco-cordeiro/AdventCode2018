using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day2
{
    public class Tests
    { 
        [Test]
        [TestCase("aa", "aaa", ExpectedResult = 1)]
        [TestCase("aacc", "aaa", ExpectedResult = 1)]
        [TestCase("aaa", "aabbb", ExpectedResult = 2)]
        [TestCase("aabbb", "aa", "aaa", "aaccc", ExpectedResult = 9)]
        public int TestAlgorithm(params string[] ids)
        {
            var scanner = new InventoryScanner();
            var checksum = scanner.ScanBox(ids);
            return checksum;
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var box = new List<string>();
            using (var stream = new FileStream("day2/input.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    box.Add(line);
                }
            }

            var scanner = new InventoryScanner();
            var checksum = scanner.ScanBox(box.ToArray());

            Console.WriteLine(checksum);
        }

        [Test]
        public void FindSolutionToPart2()
        {
        }
    }
}