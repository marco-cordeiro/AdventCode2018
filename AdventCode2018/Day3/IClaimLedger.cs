using System.Collections.Generic;

namespace AdventCode2018.Day3
{
    public interface IClaimLedger
    {
        IEnumerable<FabricClaim> Claims { get; }
    }
}