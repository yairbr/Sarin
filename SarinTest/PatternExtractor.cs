using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SarinTest
{
    class PatternExtractor
    {
        private static string regex = @"@Country: (?<country>[a-zA-Z\s]+) Predicted Hits: (?<hitsNum>[\d]+)";

        private string country;
        private int hits;


        public PatternExtractor(string input)
        {
            Regex pattern = new Regex(regex);
            if (pattern.IsMatch(input))
            {
                Match match = pattern.Match(input);
                country = match.Groups["country"].Value;
                hits = int.Parse(match.Groups["hitsNum"].Value);
            }
            else
            {
                hits = -1;
                country = null;
            }
        }

        public int GetNumberOfHits()
        {
            return hits;
        }

        public string GetCountry()
        {
            return country;
        }



    }
}
