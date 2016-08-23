using System.Collections.Generic;

namespace SarinTest
{
    internal abstract class ContinentFileReader
    {
        internal string file;
        

        internal ContinentFileReader(string file)
        {
            this.file = file;
        }


        internal abstract Dictionary<string, List<string>> LoadContinents();
    }
}