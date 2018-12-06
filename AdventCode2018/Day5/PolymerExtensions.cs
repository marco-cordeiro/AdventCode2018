namespace AdventCode2018.Day5
{
    public static class PolymerExtensions
    {
        public static bool IsPositive(this char unit)
        {
            var isotope = (int)unit;
            return isotope > 64 && isotope < 91;
        }

        public static bool IsNegative(this char unit)
        {
            var isotope = (int)unit;
            return isotope > 96 && isotope < 123;
        }
        public static bool IsValidIsotope(this char unit)
        {
            var isotope = (int)unit;
            return isotope > 96 && isotope < 123 || isotope > 64 && isotope < 91;
        }
        public static bool ReactsWith(this char unit1, char unit2)
        {
            return unit1.SameType(unit2) &&
                   (unit1.IsNegative() && unit2.IsPositive() || unit1.IsPositive() && unit2.IsNegative());
        }

        public static bool SameType(this char unit1, char unit2)
        {
            return unit1 == unit2 || (unit1 + 32) == unit2 || unit1 == (unit2 + 32);
        }
    }
}