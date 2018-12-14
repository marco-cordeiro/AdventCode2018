using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCode2018.Day10
{
    public class StarMap
    {
        private const string RegularExpression = @"position=\<(?<pos_x>[\d\s-]*), (?<pos_y>[\d\s-]*)> velocity=\<(?<vel_x>[\d\s-]*), (?<vel_y>[\d\s-]*)\>";
        private static readonly Regex Regex = new Regex(RegularExpression);
        private Star[] _stars;
        
        public void Load(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(LoadData(stream));
            }
        }

        public void Load(IEnumerable<string> lines)
        {
            _stars = lines.Select(ParseEvent).ToArray();
        }

        public void Print(int second)
        {
            var map = CalculateStarsMap(second);
            
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y] ? '#' : '.');
                }
                Console.WriteLine();
            }
        }

        public int FindStarsOptimalPosition(int second = 0)
        {
            var previous_min_size_X = int.MaxValue;
            var previous_min_size_Y = int.MaxValue;
            var min_size_X = int.MaxValue-1;
            var min_size_Y = int.MaxValue-1;

            int size_X = 0;
            int size_Y = 0;
            int offset_X =0;
            int offset_Y =0;
            StarPosition[] starPositions = new StarPosition[0];

            while (min_size_X < previous_min_size_X && min_size_Y < previous_min_size_Y)
            {
                (size_X, size_Y, offset_X, offset_Y, starPositions) = CalculateStarPosition(second);

                if (size_X <= min_size_X && size_Y <= min_size_Y)
                {
                    previous_min_size_X = min_size_X;
                    previous_min_size_Y = min_size_Y;
                    min_size_X = size_X;
                    min_size_Y = size_Y;
                }

                if (size_X > min_size_X && size_Y > min_size_Y)
                {
                    second--;
                    break;
                }

                second++;
            }

            return second;
        }

        private bool[,] CalculateStarsMap(int second)
        {
            int size_X = 0;
            int size_Y = 0;
            int offset_X = 0;
            int offset_Y = 0;
            StarPosition[] starPositions = new StarPosition[0];
            (size_X, size_Y, offset_X, offset_Y, starPositions) = CalculateStarPosition(second);

            var map = new bool[size_X, size_Y];

            for (var i = 0; i < starPositions.Length; i++)
            {
                map[offset_X + starPositions[i].X, offset_Y + starPositions[i].Y] = true;
            }

            return map;
        }

        private (int size_X, int size_Y, int offset_X, int offset_Y, StarPosition[] positions) CalculateStarPosition(int second)
        {
            var starPositions = new StarPosition[_stars.Length];
            var max_X = int.MinValue;
            var max_Y = int.MinValue;
            var min_X = int.MaxValue;
            var min_Y = int.MaxValue;
            for (var i = 0; i < _stars.Length; i++)
            {
                starPositions[i].X = _stars[i].Position.X + _stars[i].VelocityX * second;
                starPositions[i].Y = _stars[i].Position.Y + _stars[i].VelocityY * second;

                max_X = Math.Max(max_X, starPositions[i].X);
                max_Y = Math.Max(max_Y, starPositions[i].Y);
                min_X = Math.Min(min_X, starPositions[i].X);
                min_Y = Math.Min(min_Y, starPositions[i].Y);
            }

            var offset_X = min_X * -1 + 1;
            var offset_Y = min_Y * -1 + 1;
            var size_X = max_X - min_X + 3;
            var size_Y = max_Y - min_Y + 3;

            return (size_X, size_Y, offset_X, offset_Y, starPositions);
        }

        private IEnumerable<string> LoadData(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }

        private static Star ParseEvent(string line)
        {
            var matches = Regex.Match(line);
            return new Star
            {
                Position = new StarPosition
                {
                    X = int.Parse(matches.Groups["pos_x"].Value),
                    Y = int.Parse(matches.Groups["pos_y"].Value)
                },
                VelocityX = int.Parse(matches.Groups["vel_x"].Value),
                VelocityY = int.Parse(matches.Groups["vel_y"].Value),
            };
        }

        private struct StarPosition
        {
            public int X;
            public int Y;
        }

        private struct Star
        {
            public StarPosition Position;
            public int VelocityX;
            public int VelocityY;
        }
    }
}