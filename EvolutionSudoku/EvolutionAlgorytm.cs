using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku
{
	public class EvolutionAlgorytm : ISudokuAlgorythm
	{
		private readonly AlgorytmParameters _parameters;
		private readonly SudokuBoard _board;
		private readonly List<(int row, int col)> _emptyPositions;
		private readonly Random _rnd = new Random();

		public DNA[] Population { get; private set; }

		public EvolutionAlgorytm(AlgorytmParameters parameters, SudokuBoard board)
		{
			_parameters = parameters;
			_board = board;

			_emptyPositions = new List<(int, int)>();
			for (int i = 0; i < 9; i++)
				for (int j = 0; j < 9; j++)
					if (board.Board[i, j] == 0)
						_emptyPositions.Add((i, j));

			int geneLength = _emptyPositions.Count;
			Population = new DNA[_parameters.PopulationCount];
			for (int i = 0; i < _parameters.PopulationCount; i++)
			{
				var dna = new DNA(new int[geneLength]);
				dna.SetRandomDNA();
				dna.score = _board.Fit(dna).Score();
				Population[i] = dna;
			}

			Population = Population.OrderBy(d => d.score).ToArray();
		}

		public void GenerateNextGeneration()
		{
			var nextGen = new List<DNA>(_parameters.PopulationCount);

			while (nextGen.Count < _parameters.PopulationCount)
			{
				var parent1 = Population[_rnd.Next(_parameters.SelectedBestCount)];
				var parent2 = Population[_rnd.Next(_parameters.SelectedBestCount)];

				for (int k = 0;
					 k < _parameters.ChildrenCount && nextGen.Count < _parameters.PopulationCount;
					 k++)
				{
					var child = CrossDNA(parent1, parent2);
					AdaptDNA(child);
					MutateDNA(child);
					child.score = _board.Fit(child).Score();
					nextGen.Add(child);
				}
			}

			Population = nextGen.OrderBy(d => d.score).ToArray();
		}

		private DNA CrossDNA(DNA p1, DNA p2)
		{
			int len = p1.digits.Length;
			var child = new DNA(new int[len]);

			int P = Math.Max(1, _parameters.DNAParts);
			var cuts = new HashSet<int>();
			while (cuts.Count < P - 1)
				cuts.Add(_rnd.Next(1, len));
			var sortedCuts = cuts.OrderBy(x => x).ToList();

			bool takeFromP1 = true;
			int nextCutIdx = 0;
			int nextCut = sortedCuts.Count > 0 ? sortedCuts[0] : len;

			for (int i = 0; i < len; i++)
			{
				if (i == nextCut)
				{
					takeFromP1 = !takeFromP1;
					nextCutIdx++;
					nextCut = nextCutIdx < sortedCuts.Count ? sortedCuts[nextCutIdx] : len;
				}
				child.digits[i] = (takeFromP1 ? p1 : p2).digits[i];
			}

			return child;
		}

		private void AdaptDNA(DNA dna)
		{
			var filled = _board.Fit(dna);
			var wrong = filled.WrongFields();

			for (int idx = 0; idx < dna.digits.Length; idx++)
			{
				var (r, c) = _emptyPositions[idx];
				if (!wrong[r, c]) continue;

				var used = new HashSet<int>();
				for (int j = 0; j < 9; j++)
				{
					used.Add(filled.Board[r, j]);
					used.Add(filled.Board[j, c]);
				}
				int br = (r / 3) * 3, bc = (c / 3) * 3;
				for (int i = 0; i < 3; i++)
					for (int j = 0; j < 3; j++)
						used.Add(filled.Board[br + i, bc + j]);

				var candidates = Enumerable.Range(1, 9).Where(v => !used.Contains(v)).ToList();
				if (candidates.Count > 0)
					dna.digits[idx] = candidates[_rnd.Next(candidates.Count)];
			}
		}

		private void MutateDNA(DNA dna)
		{
			for (int i = 0; i < dna.digits.Length; i++)
			{
				if (_rnd.NextDouble() < _parameters.MutationChance)
					dna.digits[i] = _rnd.Next(1, 10);
			}
		}

		public DNA GetBestDNA()
			=> Population[0];

		public PopulationStatistics GetPopulationStatistics()
		{
			var scores = Population.Select(d => d.score).ToList();
			return new PopulationStatistics
			{
				PopulationCount = scores.Count,
				PopulationScore = scores.Average(),
				BestScored = scores.Min(),
				WorstScored = scores.Max()
			};
		}

		public bool IsSolved()
			=> Population[0].score == 0;
	}
}
