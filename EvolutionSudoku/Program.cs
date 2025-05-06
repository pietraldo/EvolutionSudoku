namespace EvolutionSudoku;

public class Program
{
	static void Main(string[] args)
	{
        // Display program info
        Console.WriteLine("================================");
        Console.WriteLine(" Sudoku Solver");
        Console.WriteLine(" Author(s): Jakub Pietrzak, Grzegorz Prasek");
        Console.WriteLine(" Created: 2025-05");
        Console.WriteLine("================================\n");

        while(true)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Solve Sudoku");
            Console.WriteLine("2. Exit");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                SolveSudoku();
            }
            else if (choice == "2")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private static void SolveSudoku()
    {
        // Prompt for board file path
        Console.Write("Enter the path to the board file: ");
        string boardPath = Console.ReadLine();

        // Load board
        SudokuBoard board = new SudokuBoard();
        try
        {
            board.LoadFromFile(boardPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading board: " + ex.Message);
            return;
        }

        Console.WriteLine("\nInitial Sudoku Board:");
        board.Print();
        Console.WriteLine("Initial Score: " + board.Score());

        // Choose algorithm
        Console.WriteLine("\nSelect algorithm type:");
        Console.WriteLine("1. Genetic");
        Console.WriteLine("2. Evolutionary");
        Console.Write("Choice (1 or 2): ");

        string choice = Console.ReadLine();
        SudokuAlgorythmType algorithmType;

        switch (choice)
        {
            case "1":
                algorithmType = SudokuAlgorythmType.Genetic;
                break;
            case "2":
                algorithmType = SudokuAlgorythmType.Evolution;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        // Solve the board
        SudokuSolver solver = new SudokuSolver();
        solver.SetBoard(board);

        Console.WriteLine("\nSolving the Sudoku board...");
        DNA dna = solver.Solve(algorithmType);

        Console.WriteLine("\nBest DNA Result:");
        dna.Print();

        SudokuBoard solvedBoard = board.Fit(dna);
        Console.WriteLine("\nSolved Sudoku Board:");
        solvedBoard.Print();

        Console.WriteLine("Final Score: " + solvedBoard.Score());
        Console.WriteLine("==============================");
    }
}

