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
			AlgorytmParameters algorytmParameters = new AlgorytmParameters(1000,20,0.1f,1000, 0.3f);

			ISudokuAlgorythm algorythm = new GeneticAlgorytm(algorytmParameters, board);
			algorythm.GetPopulationStatistics().Print();

			int iterations = 0;
			while (!algorythm.IsSolved() && iterations<algorytmParameters.MaxGenerations)
			{
				algorythm.GenerateNextGeneration();
				algorythm.GetPopulationStatistics().Print();

				iterations++;
			}
			return new DNA(new int[81]);
		}
	}
}
