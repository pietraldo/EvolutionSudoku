using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku
{
	public class SudokuSolver
	{
		public SudokuBoard board;
		public SudokuSolver() 
		{ 
		
		}
		public void SetBoard(SudokuBoard board)
		{
			this.board = board;
		}
		public DNA Solve()
		{
			AlgorytmParameters algorytmParameters = new AlgorytmParameters(5,2,0.1f,10,2);

			ISudokuAlgorythm algorythm = new GeneticAlgorytm(algorytmParameters, board);
			algorythm.GetPopulationStatistics().Print();
			while (!algorythm.IsSolved())
			{
				algorythm.GenerateNextGeneration();
				algorythm.GetPopulationStatistics().Print();
			}
			return new DNA(new int[81]);
		}
	}
}
