using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventCode2018.Day7;
using NUnit.Framework;

namespace AdventCode2018.Day8
{
    public class Tests
    {
        [Test]
        [TestCase("Step B must be finished before step G can begin.", ExpectedResult = "GB")]

        public string TestDependencyLoader(string line)
        {
            var magician = new InstructionMagician();
            magician.Load(new[] {line});

            return magician.SortInstructions();
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var magician = new InstructionMagician();
            magician.Load("Day7/input.txt");

            var instructions = magician.SortInstructions();

            Assert.That(instructions, Is.EqualTo("BGJCNLQUYIFMOEZTADKSPVXRHW"));
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var magician = new InstructionMagician();
            magician.Load("Day7/input.txt");

            var instructions = magician.CalculateConstructionTime(5);

            Assert.That(instructions, Is.EqualTo(1017));
        }
    }
}