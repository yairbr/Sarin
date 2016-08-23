using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SarinTest
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length < 2)
                throw new Exception("Not enough arguments");

            string contFile = args[0];
            string txtFile = args[1];
            if (!File.Exists(contFile) || !File.Exists(txtFile))
                throw new InvalidDataException("Invalid arguments");
            if(!Path.GetExtension(contFile).Equals(".xls") && !Path.GetExtension(contFile).Equals(".txt"))
                throw new InvalidDataException("Invalid Continent file");

            HitsList lst = new HitsList(contFile);
            lst.ReadHitsListFromFile(txtFile);
            Console.WriteLine(lst.CreateJson());
            Console.Read();
        }
    }
}
