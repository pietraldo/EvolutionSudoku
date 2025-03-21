namespace EvolutionSudoku;

public class Program
{
	static void Main(string[] args)
	{
		SudokuBoard board = new SudokuBoard();
		board.LoadFromFile("C:/Users/pietr/Downloads/board.txt");
		board.Print();

		SudokuSolver solver = new SudokuSolver();
		solver.SetBoard(board);
		DNA dna = solver.Solve();
		dna.Print();
	}
}

