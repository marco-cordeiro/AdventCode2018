using System;

namespace AdventCode2018.Day6
{
    public class SpatialMap
    {
        private readonly short[,] _map;
        private readonly Coordinate[] _coords;

        public SpatialMap(int size, Coordinate[] coords)
        {
            _map = new short[size, size];
            _coords = coords;
        }

        public SpatialMap CheckBoundaries()
        {
            for (int k = 0; k < _coords.Length; k++)
            {
                _coords[k].Finite = CheckBoundaries(_coords[k]);
            }

            return this;
        }

        public int CalculateDistances(int limit, bool print = false)
        {
            var mapProjection = new int[_map.GetLength(0), _map.GetLength(1)];
            var area = 0;

            for (int y = 0; y < _map.GetLength(1); y++)
            {
                for (int x = 0; x < _map.GetLength(0); x++)
            {
                    foreach (var coordinate in _coords)
                    {
                        var distance = Math.Abs(x - coordinate.X) + Math.Abs(y - coordinate.Y);

                        mapProjection[x, y] += distance;

                        if (mapProjection[x, y] > limit)
                            break;
                    }

                if (mapProjection[x, y] <= limit)
                    area++;

                    if (print)
                        Console.Write(mapProjection[x, y] > limit ? '.' : '#');
                }
                if (print)
                    Console.WriteLine();
            }

            return area;
        }

        public SpatialMap CalculateAreas()
        {
            CalculateDistanceToClosestPoint();

            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    if (_map[x, y] < 0) continue;

                    _coords[_map[x, y]].Area++;
                }
            }

            return this;
        }

        public void Print()
        {
            for (int y = 0; y < _map.GetLength(1); y++)
            {
                for (int x = 0; x < _map.GetLength(0); x++)
                {
                    Console.Write(_map[x, y].ToChar());
                }
                Console.WriteLine();
            }
        }

        private void CalculateDistanceToClosestPoint()
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    var closestPoint = -1;
                    var distanceOfClosestPoint = int.MaxValue;

                    for (int i = 0; i < _coords.Length; i++)
                    {
                        var distance = Math.Abs(x - _coords[i].X) + Math.Abs(y - _coords[i].Y);
                        if (distanceOfClosestPoint > distance)
                        {
                            closestPoint = i;
                            distanceOfClosestPoint = distance;
                            continue;
                        }

                        if (distanceOfClosestPoint == distance)
                        {
                            closestPoint = -1;
                        }
                    }

                    _map[x, y] = (short)closestPoint;
                }
            }
        }

        private bool CheckBoundaries(Coordinate coords)
        {
            var bounded = false;
            for (int i = coords.X - 1; i >= 0; i--)
            {
                if (_map[i, coords.Y] == coords.Id)
                {
                    continue;
                }

                bounded = true;
                break;
            }
            if (!bounded) return bounded;

            bounded = false;
            for (int i = coords.X + 1; i < _map.GetLength(0); i++)
            {
                if (_map[i, coords.Y] == coords.Id)
                {
                    continue;
                }

                bounded = true;
                break;
            }
            if (!bounded) return bounded;

            bounded = false;
            for (int i = coords.Y - 1; i >= 0; i--)
            {
                if (_map[coords.X, i] == coords.Id)
                {
                    continue;
                }

                bounded = true;
                break;
            }
            if (!bounded) return bounded;

            bounded = false;
            for (int i = coords.Y + 1; i < _map.GetLength(1); i++)
            {
                if (_map[coords.X, i] == coords.Id)
                {
                    continue;
                }

                bounded = true;
                break;
            }
            return bounded;
        }
    }
}