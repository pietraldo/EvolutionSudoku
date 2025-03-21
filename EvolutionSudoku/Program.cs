namespace EvolutionSudoku;

public class Program
{
	static void Main(string[] args)
	{
		SudokuBoard board = new SudokuBoard();
		board.LoadFromFile(
			"C:/Users/pietr/Documents/studia/wstep_do_sztucznej_inteligencji/EvolutionSudoku/Example_Boards/empty.txt");
		board.Print();
        Console.WriteLine(board.Score());


		SudokuSolver solver = new SudokuSolver();
		solver.SetBoard(board);
		DNA dna = solver.Solve();
		dna.Print();
		
		SudokuBoard solvedBoard=board.Fit(dna);
		solvedBoard.Print();
        Console.WriteLine("BoardScore: "+solvedBoard.Score());
    }
}

