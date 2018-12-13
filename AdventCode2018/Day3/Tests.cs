using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day3
{
    public class Tests
    { 
        [Test]
        [TestCase("#1 @ 1,1: 2x2", "#2 @ 2,2: 2x2", ExpectedResult = 1)]
        public int TestAlgorithm(params string[] claims)
        {
            var fabric = new byte[10,10];
            var claimLedger = new ClaimLedger();
            claimLedger.Add(claims);
            fabric.Allocate(claimLedger);

            var requested = fabric.CheckOverAllocation();

            return requested;
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var fabric = new byte[1000, 1000];
            var claimLedger = new ClaimLedger();
            claimLedger.LoadClaims("day3/input.txt");

            fabric.Allocate(claimLedger);

            var overAllocatedArea = fabric.CheckOverAllocation();

            Assert.That(overAllocatedArea, Is.EqualTo(103482));
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var fabric = new byte[1000, 1000];
            var claimLedger = new ClaimLedger();
            claimLedger.LoadClaims("day3/input.txt");

            fabric.Allocate(claimLedger);

            Assert.That(claimLedger.Claims.Count(x => !fabric.IsOverAllocated(x)), Is.EqualTo(1));
            Assert.That(claimLedger.Claims.First(x => !fabric.IsOverAllocated(x)).Id, Is.EqualTo(686));
        }
    }
}