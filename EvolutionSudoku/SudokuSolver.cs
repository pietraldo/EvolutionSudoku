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
			ISudokuAlgorythm algorythm = new EvolutionAlgorytm(new AlgorytmParameters());
			while(!algorythm.IsSolved())
			{
				algorythm.GenerateNextGeneration();
				algorythm.GetPopulationStatistics().Print();
			}
			return new DNA(new int[81]);
		}
	}
}
