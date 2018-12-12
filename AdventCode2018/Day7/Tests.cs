using NUnit.Framework;

namespace AdventCode2018.Day7
{
    public class Tests
    {
        [Test]
        [TestCase("Step C must be finished before step A can begin.", ExpectedResult = "CA")]
        [TestCase("Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin.", ExpectedResult = "CABDFE")]

        public string TestMagicianSorting(params string[] lines)
        {
            var magician = new InstructionMagician();
            magician.Load(lines);

            return magician.SortInstructions();
        }

        [Test]
        [TestCase("Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin.", ExpectedResult = 15)]
        public int TestMagicianTimeCalculation(params string[] lines)
        {
            var magician = new InstructionMagician();
            magician.Load(lines);

            return magician.CalculateConstructionTime(2);
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
            var magician = new InstructionMagician(60);
            magician.Load("Day7/input.txt");

            var instructions = magician.CalculateConstructionTime(5);

            Assert.That(instructions, Is.EqualTo(1017));
        }
    }
}