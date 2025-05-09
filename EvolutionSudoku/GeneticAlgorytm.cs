﻿using System;
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
	public AlgorytmParameters AlgorytmParameters { get; set; }

	public GeneticAlgorytm(AlgorytmParameters algorytmParameters, SudokuBoard board)
	{
		// generates randomly population
		Population = new DNA[algorytmParameters.PopulationCount];
		Board = board;
		this.AlgorytmParameters = algorytmParameters;
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
        List<DNA> childrens = new List<DNA>();
		while(childrens.Count< AlgorytmParameters.PopulationCount)
		{
			int RandomIndex1 = new Random().Next(0, AlgorytmParameters.SelectedBestCount);
			int RandomIndex2 = new Random().Next(0, AlgorytmParameters.SelectedBestCount);

			DNA parent1 = Population[RandomIndex1];
			DNA parent2 = Population[RandomIndex2];

			DNA child = CrossDNA(parent1, parent2);
			MutateDNA(child);
			childrens.Add(child);
		}
		Population = childrens.ToArray();

		foreach (DNA dna in Population)
		{
			dna.score = Board.Fit(dna).Score();
		}
		Population = Population.OrderBy(dna => dna.score).ToArray();
		
		Console.WriteLine(Population[0].score);
	}

	public void MutateDNA(DNA dna)
	{
        for (int i = 0; i < dna.digits.Length; i++)
		{
			if (new Random().NextDouble() < AlgorytmParameters.MutationChance)
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
			if(new Random().NextDouble() < AlgorytmParameters.SplitChance)
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
			scores.Add(Board.Fit(dna).Score());
		}

        return new PopulationStatistics
        {
            PopulationCount = scores.Count,
            PopulationScore = scores.Average(),
            BestScored = scores.Min(),
            WorstScored = scores.Max()
        };
    }

	public int BestScore()
	{
		return Population[0].score;
	}

	public bool IsSolved()
	{
		return Population[0].score == 0;
	}

	public DNA GetBestDNA()
	{
		return Population[0];
	}
}
