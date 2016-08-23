using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace SarinTest
{
    class CountriesSet
    {
        private Dictionary<string, List<string>> m_continent_countries;
        private ContinentFileReader m_freader;

        public CountriesSet(string file)
        {
            m_continent_countries = new Dictionary<string, List<string>>();
            m_freader = null;

            if (Path.GetExtension(file).Equals(".xls"))
                m_freader = new ExcelReader(file);
            else if (Path.GetExtension(file).Equals(".txt"))
                m_freader = new TextFileReader(file);
            LoadContinents();
        }

        public string GetContainingContinent(string country)
        {
            foreach (string continent in m_continent_countries.Keys)
            {
                if (m_continent_countries[continent].Contains(country))
                {
                    return continent;
                }
            }
            return null;
        }

        private void LoadContinents()
        {
            m_continent_countries = m_freader.LoadContinents();
        }
    }
}
