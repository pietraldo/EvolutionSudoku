using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;
public interface ISudokuAlgorythm
{
	public void GenerateNextGeneration();
	public PopulationStatistics GetPopulationStatistics();
	public bool IsSolved();
}

