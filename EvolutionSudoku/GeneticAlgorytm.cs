using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;
public class GeneticAlgorytm : ISudokuAlgorythm
{
	public DNA[] Population;
	public readonly SudokuBoard Board;

	public GeneticAlgorytm(AlgorytmParameters algorytmParameters, SudokuBoard board)
	{
		// generates randomly population
		Population = new DNA[algorytmParameters.PopulationCount];
		Board = board;
		for(int i = 0; i < algorytmParameters.PopulationCount; i++)
		{
			Population[i] = new DNA(new int[board.CountEmptyFields()]);
		}
		foreach (DNA dna in Population)
		{
			dna.SetRandomDNA();
		}
	}

	public void GenerateNextGeneration()
	{
		throw new NotImplementedException();
	}

	public PopulationStatistics GetPopulationStatistics()
	{
		throw new NotImplementedException();
	}

	public bool IsSolved()
	{
		return false;
	}
}
