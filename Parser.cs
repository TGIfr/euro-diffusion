using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace euro_diffusion
{
    class Parser
    {
        private const string DefaultPath = @"D:\projects\repos\euro-diffusion\input.txt";

        public List<List<Country>> ReadInputFile(string path = DefaultPath)
        {
            var result = new List<List<Country>>();
            var input = ReadFile(path);
            var endOfInputFlag = false;
            var caseFlag = false;
            int numberOfCountries = 0;
            List<Country> curList = null;

            foreach (var line in input)
            {
                var trimmedLine = line.Trim();

                if (trimmedLine == "")
                    continue;

                if (trimmedLine == "0")
                {
                    endOfInputFlag = true;
                    continue;
                }

                if (caseFlag)
                {
                    var args = line.Split(" ");
                    if (args.Length != 5)
                        throw new ArgumentException("wrong number of arguments in country line");
                    var name = args[0];
                    var xl = int.Parse(args[1]);
                    var yl = int.Parse(args[2]);
                    var xh = int.Parse(args[3]);
                    var yh = int.Parse(args[4]);
                    curList.Add(new Country(name, xl, yl, xh, yh));

                    if (--numberOfCountries == 0)
                    {
                        result.Add(curList);
                        caseFlag = false;
                    }
                } else
                {
                    if (trimmedLine.All(char.IsDigit))
                        numberOfCountries = int.Parse(line);
                    else
                        throw new ArgumentException("wrong line with number of countries");

                    curList = new List<Country>();
                    caseFlag = true;
                }


            }

            if (!endOfInputFlag)
                throw new ArgumentException("No 0 at the end of file");

            return result;
        }

        private List<string> ReadFile(string path)
        {
            using StreamReader file = new StreamReader(path);
            string line;
            var result = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            return result;
        }
    }
}
