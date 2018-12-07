namespace AdventCode2018.Day3
{
    public struct FabricClaim
    {
        public FabricClaim(int id, int left, int top, int height, int width)
        {
            Id = id;
            Left = left;
            Top = top;
            Height = height;
            Width = width;
        }

        public int Id;
        public int Left;
        public int Top;
        public int Height;
        public int Width;
    }
}