using System.Linq;
using NUnit.Framework;

namespace AdventCode2018.Day9
{
    public class Tests
    {
        [Test]
        [TestCase( 9, 26, ExpectedResult = 32)]
        [TestCase(10, 1618, ExpectedResult = 8317)]
        [TestCase(13, 7999, ExpectedResult = 146373)]
        [TestCase(17, 1104, ExpectedResult = 2764)]
        [TestCase(21, 6111, ExpectedResult = 54718)]
        [TestCase(30, 5807, ExpectedResult = 37305)]

        public long TestChecksum(int players, int lastMarbleValue)
        {
            var game = new MarbleGame(players);

            (int winner, long score) = game.Play(lastMarbleValue);

            return score;
        }

        [Test]
        public void FindSolutionToPart1()
        {
            var game = new MarbleGame(441);

            (int winner, long score) = game.Play(71032);

            Assert.That(score ,Is.EqualTo(393229));
        }

        [Test]
        public void FindSolutionToPart2()
        {
            var game = new MarbleGame(441);

            (int winner, long score) = game.Play(7103200);

            Assert.That(score, Is.EqualTo(3273405195L));
        }
    }
}