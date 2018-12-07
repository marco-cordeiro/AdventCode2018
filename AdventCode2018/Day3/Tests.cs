using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day3
{
    public class Tests
    { 
        [Test]
        [TestCase("#1 @ 1,1: 2x2", "#2 @ 2,2: 2x2", ExpectedResult = 2)]
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

            Console.WriteLine(overAllocatedArea);
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var fabric = new byte[1000, 1000];
            var claimLedger = new ClaimLedger();
            claimLedger.LoadClaims("day3/input.txt");

            fabric.Allocate(claimLedger);

            foreach (var claim in claimLedger.Claims)
            {
                if (fabric.IsOverAllocated(claim)) continue;

                Console.WriteLine(claim.Id);
                break;
            }
        }
    }
}