using System;
using System.Collections.Generic;
using System.Reflection;

namespace euro_diffusion
{
    class Program
    {
        static void Main(string[] args)
        {

            Parser parser = new Parser();

            List<List<Country>> res = null;
            try
            {
                res = parser.ReadInputFile();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            int caseNumber = 1;
            foreach (var caseToSolve in res)
            {
                Console.WriteLine($"Case Number {caseNumber++}");
                try
                {
                    var grid = new Grid(caseToSolve);
                    Console.Write(grid.SolveCase());
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }


        }
    }
}
