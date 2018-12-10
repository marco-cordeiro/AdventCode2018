namespace AdventCode2018.Day6
{
    public static class PrimitiveExtensions
    {
        public static char ToChar(this short @char)
        {
            if (@char < 0)
                return '.';

            var result = @char + 65;
            result += @char > 25 ? 6 : 0;
            result += @char > 51 ? -74 : 0;
            result = @char > 100 ? '*' : result;

            return (char)result;
        }
    }
}