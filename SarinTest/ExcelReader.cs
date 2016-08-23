using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace SarinTest
{
    class ExcelReader : ContinentFileReader
    {

        private Dictionary<string, List<string>> m_continent_countries;


        public ExcelReader(string file) : base(file)
        {
            m_continent_countries = new Dictionary<string, List<string>>();
        }

        internal override Dictionary<string, List<string>> LoadContinents()
        {

            Microsoft.Office.Interop.Excel.Application excel = null;
            Excel.Workbook wb = null;

            try
            {

                excel = new Microsoft.Office.Interop.Excel.Application();
                wb = excel.Workbooks.Open(file);
                Excel.Worksheet sheet = wb.Sheets["Table 9"] as Excel.Worksheet;

                Excel.Range range = null;

                if (sheet != null)
                    range = sheet.get_Range("A3", "I3");

                if (range != null)
                {
                    System.Array myvalues = (System.Array)range.Cells.Value;

                    foreach (var elem in myvalues)
                    {
                        if (elem != null)
                        {
                            m_continent_countries.Add(elem.ToString(), new List<string>());
                        }
                    }

                    buildForEach(sheet);
                }

                return m_continent_countries;

            }
            catch (Exception ex)
            {
                //if you need to handle stuff
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (wb != null)
                    wb.Close();
            }

            return null;
        }

        private void buildForEach(Excel.Worksheet sheet)
        {
            if (sheet == null)
                return;

            Dictionary<string, string> charToCont = new Dictionary<string, string>();
            string[] relevant_columns = new string[5];
            relevant_columns[0] = "A";
            relevant_columns[1] = "C";
            relevant_columns[2] = "E";
            relevant_columns[3] = "G";
            relevant_columns[4] = "I";
            List<string> contin = new List<string>(m_continent_countries.Keys);

            for (int i = 0; i < 5; i++)
            {
                charToCont.Add(relevant_columns[i], contin[i]);
            }
            try
            {
                foreach (string c in relevant_columns)
                {
                    BuildCountriesList(sheet, charToCont[c], c);
                }
            }
            catch
            {
            }
        }

        private void BuildCountriesList(Microsoft.Office.Interop.Excel.Worksheet sheet, string cont, string c)
        {
            bool isPrevNull = false;
            Microsoft.Office.Interop.Excel.Range range = null;
            if (sheet != null)
                range = sheet.get_Range(c + "3", c + "66");

            if (range != null)
            {
                System.Array myvalues = (System.Array)range.Cells.Value;
                foreach (var elem in myvalues)
                {
                    if (elem == null)
                        isPrevNull = true;
                    else if (elem != null && isPrevNull)
                    {
                        isPrevNull = false;
                        continue;
                    }
                    else
                        m_continent_countries[cont].Add(elem.ToString());
                }
            }
        }
    }
}
