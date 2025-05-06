using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku
{
    public enum SudokuAlgorythmType
    {
        Genetic,
        Evolution
    }

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
        public DNA Solve(SudokuAlgorythmType sudokuAlgorythmType)
        {
            float crossoverChance = 0.2f;
            float mutationChance = 0.1f;
            int startPopulation = 1000;
            int choosenCount = 10;
            int maxGenerations = 5000;

            AlgorytmParameters algorytmParameters = new AlgorytmParameters(startPopulation, choosenCount, mutationChance, maxGenerations, crossoverChance, 1, 1);

            ISudokuAlgorythm algorythm;

            if (sudokuAlgorythmType == SudokuAlgorythmType.Genetic)
                algorythm = new GeneticAlgorytm(algorytmParameters, board);
            else
                algorythm = new EvolutionAlgorytm(algorytmParameters, board);

            algorythm.GetPopulationStatistics().Print();

            int bestScore = int.MaxValue;
            int iterations = 0;
            int prevprevScore = int.MaxValue;
            int prevScore = int.MaxValue;
            while (!algorythm.IsSolved() && iterations < algorytmParameters.MaxGenerations)
            {
                algorythm.GenerateNextGeneration();
                var populationStatistics = algorythm.GetPopulationStatistics();


                int iterationBestScore = populationStatistics.BestScored;
                if (iterationBestScore < bestScore)
                    bestScore = iterationBestScore;

                // if 3 at row scores where the same then increase mutation chance to not get stuck in local minimum
                if (iterationBestScore == prevScore && prevprevScore == prevScore)
                {
                    algorythm.AlgorytmParameters.MutationChance = 0.8f;
                    Console.WriteLine("Big mutation");
                }
                else
                {
                    algorythm.AlgorytmParameters.MutationChance = mutationChance;
                }
                prevprevScore = prevScore;
                prevScore = iterationBestScore;

                iterations++;

                populationStatistics.Print();
            }
            Console.WriteLine("Best score: " + bestScore);
            return algorythm.GetBestDNA();
        }
    }
}
