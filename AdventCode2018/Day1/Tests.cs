using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day1
{
    public class Tests
    { 
        [Test]
        [TestCase(1,2,3, ExpectedResult = 6)]
        [TestCase(-1,-2,3, ExpectedResult = 0)]
        [TestCase(-4,2,-3, 7, 5, -8,2, ExpectedResult = 1)]
        public int Test1(params int[] args)
        {
            var watch = new TimeTravelWatch();
            var frequencyReader = new FrequencyReader(args);

            return watch.Calibrate(frequencyReader, true);
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var watch = new TimeTravelWatch();
            var frequencyReader = new FileFrequencyReader("Day1/input.txt");

            var result =  watch.Calibrate(frequencyReader, true);

            Console.WriteLine(result);
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var watch = new TimeTravelWatch();
            var frequencyReader = new FileFrequencyReader("Day1/input.txt");

            var result = watch.Calibrate(frequencyReader);

            Console.WriteLine(watch.Frequency);
        }
    }
}