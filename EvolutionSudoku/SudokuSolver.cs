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
			float mutationChance = 0.1f;
			AlgorytmParameters algorytmParameters = new AlgorytmParameters(10000,100,mutationChance,5000, 0.1f);

			GeneticAlgorytm algorythm = new GeneticAlgorytm(algorytmParameters, board);
			algorythm.GetPopulationStatistics().Print();

			int bestScore = algorythm.BestScore();
			int iterations = 0;
			int prevprevScore = algorythm.BestScore();
			int prevScore = algorythm.BestScore();
			while (!algorythm.IsSolved() && iterations<algorytmParameters.MaxGenerations)
			{
				algorythm.GenerateNextGeneration();
				algorythm.GetPopulationStatistics().Print();

				int iterationBestScore = algorythm.BestScore();
				if (iterationBestScore < bestScore)
				{
					bestScore = iterationBestScore;
				}

				if (iterationBestScore == prevScore && prevprevScore==prevScore)
				{
					algorythm.algorytmParameters.MutationChance = 0.8f; 
					Console.WriteLine("Big mutation");
                }
				else
				{
					algorythm.algorytmParameters.MutationChance = mutationChance;
				}
				prevprevScore = prevScore;
				prevScore = iterationBestScore;

				iterations++;
			}
            Console.WriteLine("Best score: "+bestScore);
            return algorythm.GetBestDNA();
		}
	}
}
