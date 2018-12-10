using System;
using System.Linq;
using AdventCode2018.Day3;
using NUnit.Framework;

namespace AdventCode2018.Day6
{
    public class Tests
    {
        [Test]
        public void TestToChar()
        {
            for (short i = 0; i < 60; i++)
            {
                Console.WriteLine($"{i:2} - {i.ToChar()}");
            }
        }
        
        [Test]
        [TestCase("1, 1","3, 3")]
        [TestCase("1, 1","9, 9", "5,5")]
        public void TestAlgorithm(params string[] data)
        {
            var coords = Loader.Load(data).ToArray();
            var map = new SpatialMap(10, coords);
            map.CalculateAreas().Print();
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var coords = Loader.Load("day6/input.txt").ToArray();

            var map = new SpatialMap(400, coords);
            map.CalculateAreas()
                .CheckBoundaries()
                .Print();

            var maxArea = coords.Where(x => x.Finite).Max(x => x.Area);
            var result = coords.First(x => x.Finite && x.Area == maxArea);

            Console.WriteLine($"Answer: {result.Id.ToChar()}({result.X},{result.Y}) area : {result.Area}");
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var coords = Loader.Load("day6/input.txt").ToArray();

            var map = new SpatialMap(400, coords);
            var result = map.CalculateDistances(10000, true);

            Assert.That(result, Is.EqualTo(50530));
        }
    }
}