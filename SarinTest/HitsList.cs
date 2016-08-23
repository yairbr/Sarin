using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace SarinTest
{
    class HitsList
    {
        private Dictionary<string, int> m_country_hits;
        private string m_contMost;

        CountriesSet m_countries;
        
        public HitsList(string excelFile)
        {
            m_country_hits = new Dictionary<string, int>();
            m_countries = new CountriesSet(excelFile);
            m_contMost = "";
        }

        public void ReadHitsListFromFile(string file)
        {
            Dictionary<string, int> continents_hits = new Dictionary<string, int>();
            using (StreamReader sr = File.OpenText(file))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    PatternExtractor ext = new PatternExtractor(s);
                    if(ext.GetNumberOfHits() != -1 && ext.GetCountry() != null)
                    {
                        m_country_hits.Add(ext.GetCountry(), ext.GetNumberOfHits());
                        string cont = m_countries.GetContainingContinent(ext.GetCountry());

                        if(cont != null)
                        {
                            if (!continents_hits.ContainsKey(cont))
                            {
                                continents_hits.Add(cont, 0);
                            }
                            continents_hits[cont] += ext.GetNumberOfHits();
                        }
                    }
                }
            }
            m_country_hits = m_country_hits.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            continents_hits = continents_hits.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            if(continents_hits.Count() > 0)
            {
                m_contMost = new List<string>(continents_hits.Keys)[0];
            }
        }

        public string CreateJson()
        {

            StringBuilder json = new StringBuilder("{\n");
            string ljson = JsonConvert.SerializeObject(m_country_hits, Formatting.Indented);
            json.Append(ljson).Append(" ,\n").Append("ContinentWithMostHits : ").Append("\'").Append(m_contMost)
                .Append("\' \n").Append("}");
            return json.ToString();
        }
    }
}
