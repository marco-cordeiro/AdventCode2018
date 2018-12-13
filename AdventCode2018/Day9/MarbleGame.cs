using System;

namespace AdventCode2018.Day9
{
    public class MarbleGame
    {
        private readonly int _players;

        public MarbleGame(int players)
        {
            _players = players;
        }

        public (int winner, long score) Play(int lastMarbleValue)
        {
            var marbles = lastMarbleValue + 1;
            var score = new long[_players];

            var zeroMarble = new Marble(0);
            var currentMarble = zeroMarble;
            currentMarble.Left = currentMarble;
            currentMarble.Right = currentMarble;

            for (var i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    var m = RotateAntiClockwise(currentMarble, 7);

                    score[i % _players] += i + m.Value;

                    m.Left.Right = m.Right;
                    m.Right.Left = m.Left;
                    currentMarble = m.Right;

                    continue;
                }

                var m1 = currentMarble.Right;
                var m2 = m1.Right;
                currentMarble = new Marble(i) {Left = m1, Right = m2};
                m1.Right = currentMarble;
                m2.Left = currentMarble;
            }

            var player = 0;
            var maxScore = 0L;
            for (int i = 0; i < _players; i++)
            {
                if (score[i] > maxScore)
                {
                    player = i + 1;
                    maxScore = score[i];
                }

                Console.WriteLine($"[{i + 1}] {score[i]}");
            }
            return (player,  maxScore);
        }

        private void Print(Marble zeroMarble, Marble current, int i)
        {
            Console.Write($"{i}");

            var marble = zeroMarble;
            while(true)
            {
                Console.Write(marble == current ? $" ({marble.Value})" : $" {marble.Value}");
                marble = marble.Right;

                if (marble == zeroMarble)
                    break;
            }
            Console.WriteLine();
        }

        private static Marble RotateAntiClockwise(Marble marble, int pos)
        {
            for (int i = 0; i < 7; i++)
            {
                marble = marble.Left;
            }

            return marble;
        }

        private class Marble
        {
            public Marble(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public Marble Left { get; set; }
            public Marble Right { get; set; }
        }
    }
}