using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku
{
	public class PopulationStatistics
	{
		public int PopulationCount;
		public double PopulationScore;
		public int BestScored;
		public int WorstScored;

		public PopulationStatistics() { }
        public void Print()
        {
            Console.WriteLine("===== Population Statistics =====");
            Console.WriteLine($"{"Population Count:",-20} {PopulationCount}");
            Console.WriteLine($"{"Population Score:",-20} {PopulationScore:F2}");
            Console.WriteLine($"{"Best Scored:",-20} {BestScored}");
            Console.WriteLine($"{"Worst Scored:",-20} {WorstScored}");
            Console.WriteLine("=================================");
        }
    }
}
