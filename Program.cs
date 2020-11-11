using System;
using System.Reflection;

namespace euro_diffusion
{
    class Program
    {
        static void Main(string[] args) {
            Parser parser = new Parser();

            var res = parser.ReadInputFile();
            int caseNumber = 1;
            foreach (var caseToSolve in res)
            {
                Console.WriteLine($"Case Number {caseNumber++}");
                var grid = new Grid(caseToSolve);
                Console.Write(grid.SolveCase());
            }
        }
    }
}
