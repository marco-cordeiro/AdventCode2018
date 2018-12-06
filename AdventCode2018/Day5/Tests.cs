using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day5
{
    public class Tests
    { 
        [Test]
        [TestCase("aA", ExpectedResult = "")]
        [TestCase("abAB", ExpectedResult = "abAB")]
        [TestCase("abBA", ExpectedResult = "")]
        [TestCase("dabAcCaCBAcCcaDA", ExpectedResult = "dabCBAcaDA")]
        [TestCase("dabAcCaCBAcCcaACDA", ExpectedResult = "dabCBADA")]
        [TestCase("abcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBAabcdefghijklmnopQrStuVxzZXvUTsRqPONMLKJIHGFEDCBA", ExpectedResult = "", TestName = "really long and recursive")]
        public string Test1(string polymer)
        {
            var watch = new Stopwatch();
            var optimizer = new PolymerOptimizer();

            watch.Start();
            var result =  optimizer.Optimize(polymer);
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);

            return result;
        }

        [Test]
        public void FindSolutionToPart1()
        {
            string content;
            using (var stream = new FileStream("Day5/adventofcode.com_2018_day_5_input.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                content = reader.ReadToEnd();
            }

            var optimizer = new PolymerOptimizer();

            var result = optimizer.Optimize(content);

            Console.WriteLine(result.Length);
        }

        [Test]
        public void FindSolutionToPart2()
        {
            string content;
            using (var stream = new FileStream("Day5/adventofcode.com_2018_day_5_input.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                content = reader.ReadToEnd();
            }

            var optimizer = new PolymerOptimizer();
            var result = new List<Tuple<char, int>>();

            var polymer = content.Select(c => c).Where(c => c.IsValidIsotope()).ToList();

            optimizer.Optimize(polymer);

            for (int i = 65; i < 91; i++)
            {
                var isotope = (char) i;
                var simplifiedPolymer = polymer.Where(c => !isotope.SameType(c)).ToList();

                optimizer.Optimize(simplifiedPolymer);

                result.Add(new Tuple<char, int>(isotope, simplifiedPolymer.Count));
            }

            var min = result.Select(x => x.Item2).Min();
            var solution =result.Where(x => x.Item2 == min).ToList();

            Assert.That(solution, Has.Count.EqualTo(1));
            Console.WriteLine($"'{solution[0].Item1}' :{solution[0].Item2}");
        }
    }
}