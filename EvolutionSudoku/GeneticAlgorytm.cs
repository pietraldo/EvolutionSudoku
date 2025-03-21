using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;
public class GeneticAlgorytm : ISudokuAlgorythm
{
	public DNA[] Population;
	public readonly SudokuBoard Board;
	AlgorytmParameters algorytmParameters;

	public GeneticAlgorytm(AlgorytmParameters algorytmParameters, SudokuBoard board)
	{
		// generates randomly population
		Population = new DNA[algorytmParameters.PopulationCount];
		Board = board;
		this.algorytmParameters = algorytmParameters;
		for (int i = 0; i < algorytmParameters.PopulationCount; i++)
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
		foreach (DNA dna in Population)
		{
			dna.score = Board.Fit(dna).Score();
			dna.Print();
            Console.WriteLine(dna.score);
		}
		Population = Population.OrderBy(dna => dna.score).ToArray();

		foreach (DNA dna in Population)
		{
			dna.Print();
			Console.WriteLine(dna.score);
		}

		List<DNA> childrens = new List<DNA>();
		while(childrens.Count<algorytmParameters.PopulationCount)
		{
			int RandomIndex1 = new Random().Next(0, algorytmParameters.SelectedBestCount);
			int RandomIndex2 = new Random().Next(0, algorytmParameters.SelectedBestCount);

			DNA parent1 = Population[RandomIndex1];
			DNA parent2 = Population[RandomIndex2];

			DNA child = CrossDNA(parent1, parent2);
			MutateDNA(child);
			childrens.Add(child);
		}
		Population = childrens.ToArray();
	}

	public void MutateDNA(DNA dna)
	{
		for (int i = 0; i < dna.digits.Length; i++)
		{
			if (new Random().NextDouble() < algorytmParameters.MutationChance)
			{
				dna.digits[i] = new Random().Next(1, 10);
			}
		}
	}

	public DNA CrossDNA(DNA parent1, DNA parent2)
	{
		DNA child = new DNA(parent1.digits.Length);
		DNA parent = parent1;
		for (int i=0; i< parent1.digits.Length; i++)
		{
			if(new Random().NextDouble() < algorytmParameters.SplitChance)
			{
				if(parent==parent1)
				{
					parent = parent2;
				}
				else
				{
					parent = parent1;
				}
			}
			child.digits[i] = parent.digits[i];
		}
		//parent1.Print();
		//parent2.Print();
		//child.Print();
		return child;
	}

	public PopulationStatistics GetPopulationStatistics()
	{
		List<int> scores = new List<int>();
		foreach (DNA dna in Population)
		{
			//dna.Print();
			scores.Add(Board.Fit(dna).Score());
		}
		scores.Sort();

        Console.WriteLine(scores.First());

		return new PopulationStatistics();
	}

	public bool IsSolved()
	{
		return false;
	}
}
