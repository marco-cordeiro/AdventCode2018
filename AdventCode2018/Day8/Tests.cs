using System.IO;
using System.Text;
using NUnit.Framework;

namespace AdventCode2018.Day8
{
    public class Tests
    {
        [Test]
        [TestCase("1 1 0 2 4 78 8", ExpectedResult = 90)]
        [TestCase("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", ExpectedResult = 138)]

        public int TestChecksum(string line)
        {
            var manager = new LicenseManager();
            manager.Load(new MemoryStream(Encoding.UTF8.GetBytes(line)));

            return manager.Checksum();
        }

        [Test]
        [TestCase("1 1 0 2 4 78 1", ExpectedResult = 82)]
        [TestCase("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", ExpectedResult = 66)]

        public int TestCalculateChecksum(string line)
        {
            var manager = new LicenseManager();
            manager.Load(new MemoryStream(Encoding.UTF8.GetBytes(line)));

            return manager.CalculateChecksum();
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var manager = new LicenseManager();
            manager.Load("Day8/input.txt");

            var result = manager.Checksum();

            Assert.That(result, Is.EqualTo(36627));
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var manager = new LicenseManager();
            manager.Load("Day8/input.txt");

            var result = manager.CalculateChecksum();

            Assert.That(result, Is.EqualTo(16695));
        }
    }
}