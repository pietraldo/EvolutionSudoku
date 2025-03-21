using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;
public class AlgorytmParameters
{
	public int PopulationCount { get; set; }
	public int SelectedBestCount { get; set; }
	public float MutationChance { get; set; }
	public int MaxGenerations { get; set; }
	public float SplitChance { get; set; }

	public AlgorytmParameters()
	{
		PopulationCount = 5;
		SelectedBestCount = 2;
		MutationChance = 0.1f;
		MaxGenerations = 100;
		SplitChance = 0.1f;
	}
	public AlgorytmParameters(int populationCount, int selectedBestCount, float mutationChance, int maxGenerations, float splitDNACount)
	{
		PopulationCount = populationCount;
		SelectedBestCount = selectedBestCount;
		MutationChance = mutationChance;
		MaxGenerations = maxGenerations;
		SplitChance = splitDNACount;
	}
}

