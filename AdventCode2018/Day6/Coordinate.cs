namespace AdventCode2018.Day6
{
    public struct Coordinate
    {
        public Coordinate(short id, short x, short y)
        {
            Id = id;
            X = x;
            Y = y;
            Area = 0;
            Finite = true;
        }

        public short Id;
        public short X;
        public short Y;
        public int Area;
        public bool Finite;
    }
}