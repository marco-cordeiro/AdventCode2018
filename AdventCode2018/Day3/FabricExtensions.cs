using System;

namespace AdventCode2018.Day3
{
    public static class FabricExtensions
    {
        public static void Allocate(this byte[,] fabric, IClaimLedger claimLedger)
        {
            foreach (var claim in claimLedger.Claims)
            {
                fabric.Allocate(claim);
            }
        }

        public static void Allocate(this byte[,] fabric, FabricClaim claim)
        {
            for (var x = 0; x < claim.Height; x++)
            {
                for (var y = 0; y < claim.Width; y++)
                {
                    fabric[claim.Left + x, claim.Top + y]++;
                }
            }
        }

        public static bool IsOverAllocated(this byte[,] fabric, FabricClaim claim)
        {
            for (var x = 0; x < claim.Height; x++)
            {
                for (var y = 0; y < claim.Width; y++)
                {
                    if (fabric[claim.Left + x, claim.Top + y] != 1) return true;
                }
            }

            return false;
        }

        public static void Map(this byte[,] fabric)
        {
            for (var x = 0; x < fabric.GetLength(0); x++)
            {
                for (var y = 0; y < fabric.GetLength(0); y++)
                {
                    Console.Write(Math.Min(fabric[x, y], (byte)9));
                }
                Console.WriteLine();
            }
        }

        public static int CheckOverAllocation(this byte[,] fabric)
        {
            var overAllocatedArea = 0;

            for (var x = 0; x < fabric.GetLength(0); x++)
            {
                for (var y = 0; y < fabric.GetLength(0); y++)
                {
                    overAllocatedArea += fabric[x, y] > 1 ? 1 : 0;
                }
            }

            return overAllocatedArea;
        }
    }
}