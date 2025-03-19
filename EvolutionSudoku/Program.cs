namespace EvolutionSudoku;

public class Program
{
	static void Main(string[] args)
	{
		SudokuBoard board = new SudokuBoard();
		SudokuSolver solver = new SudokuSolver();
		solver.SetBoard(board);
		DNA dna = solver.Solve();
		dna.Print();
	}
}

