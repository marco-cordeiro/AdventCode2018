using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventCode2018.Day3
{
    public class ClaimLedger : IClaimLedger
    {
        private const string ClaimRegularExpression = @"#(?<id>\d*) @ (?<left>\d*),(?<top>\d*): (?<height>\d*)x(?<width>\d*)";
        private IList<FabricClaim> _claims = new List<FabricClaim>();

        public IEnumerable<FabricClaim> Claims => _claims;


        public void LoadClaims(string filename)
        {
            var claims = new List<FabricClaim>();
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new StreamReader(stream);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    claims.Add(ParseClaim(line));
                }
            }

            _claims = claims;
        }

        public void Add(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                _claims.Add(ParseClaim(line));
            }
        }

        public void Add(string line)
        {
            _claims.Add(ParseClaim(line));
        }

        private FabricClaim ParseClaim(string line)
        {
            Regex regex = new Regex(ClaimRegularExpression);

            var matches = regex.Match(line);

            var id = int.Parse(matches.Groups["id"].Value);
            var left = int.Parse(matches.Groups["left"].Value);
            var top = int.Parse(matches.Groups["top"].Value);
            var height = int.Parse(matches.Groups["height"].Value);
            var width = int.Parse(matches.Groups["width"].Value);

            return new FabricClaim(id, left, top, height, width);
        }
    }
}