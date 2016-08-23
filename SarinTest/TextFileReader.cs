using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarinTest
{
    class TextFileReader : ContinentFileReader
    {
        public TextFileReader(string file) : base(file)
        {
        }

        internal override Dictionary<string, List<string>> LoadContinents()
        {

            Dictionary<string, List<string>> continent_countries = new Dictionary<string, List<string>>();
            using (StreamReader sr = File.OpenText(file))
            {
                bool isNextCont = false;
                string currCont = null;
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    if(s.Equals(""))
                    {
                        isNextCont = true;
                    }
                    else if (isNextCont)
                    {
                        continent_countries.Add(s, new List<string>());
                        currCont = s;
                        isNextCont = false;
                    }
                    else
                    {
                        continent_countries[currCont].Add(s);
                    }
                }
            }
            return continent_countries;
        }
    }
}
