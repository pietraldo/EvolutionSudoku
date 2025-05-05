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
			float crossoverChance = 0.2f;
			float mutationChance = 0.1f;
			int startPopulation = 1000;
			int choosenCount = 10;
			int maxGenerations = 5000;


			AlgorytmParameters algorytmParameters = new AlgorytmParameters(startPopulation, choosenCount, mutationChance, maxGenerations, crossoverChance, 1, 1);

			GeneticAlgorytm algorythm = new GeneticAlgorytm(algorytmParameters, board);
			algorythm.GetPopulationStatistics().Print();

			int bestScore = algorythm.BestScore();
			int iterations = 0;
			int prevprevScore = algorythm.BestScore();
			int prevScore = algorythm.BestScore();
			while (!algorythm.IsSolved() && iterations < algorytmParameters.MaxGenerations)
			{
				algorythm.GenerateNextGeneration();
				algorythm.GetPopulationStatistics().Print();

				int iterationBestScore = algorythm.BestScore();
				if (iterationBestScore < bestScore)
					bestScore = iterationBestScore;

				// if 3 at row scores where the same then increase mutation chance to not get stuck in local minimum
				if (iterationBestScore == prevScore && prevprevScore == prevScore)
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
			Console.WriteLine("Best score: " + bestScore);
			return algorythm.GetBestDNA();
		}
	}
}
