using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;

public class SudokuBoard
{
	public int[,] Board = new int[9, 9];

	public SudokuBoard()
	{
		for (int i = 0; i < 9; i++)
			for (int j = 0; j < 9; j++)
				Board[i, j] = 0;
	}
	public SudokuBoard(int[,] digits)
	{

	}

	public SudokuBoard(SudokuBoard board)
	{
		for(int i =0; i < 9; i++)
		{
			for(int j = 0; j < 9; j++)
			{
				Board[i, j] = board.Board[i, j];
			}
		}
	}


	public void LoadFromFile(string filePath)
	{
		int index = 0;

		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException("File doesn't exist");
		}

		string[] lines = File.ReadAllLines(filePath);

		foreach (string line in lines)
		{
			foreach (char ch in line)
			{
				if (index >= 9 * 9)
					break;
				if (!char.IsDigit(ch))
					continue;

				Board[index / 9, index % 9] = ch - '0';
				index++;
			}
		}

		if (index < 9 * 9)
		{
			throw new Exception("Wrong board loaded");
		}
	}

	public int CountEmptyFields()
	{
		int count = 0;
		for (int i = 0; i < 9; i++)
			for (int j = 0; j < 9; j++)
				if (Board[i, j] == 0)
					count++;
		return count;
	}

	public void Print()
	{
		bool[,] wrongFields = WrongFields();
		int i, j;
		for (i = 0; i < 9; i++)
		{
			if (i % 3 == 0)
				Console.WriteLine("+++++++++++++++++++++++++++++++++++++");
			else
				Console.WriteLine("+---+---+---+---+---+---+---+---+---+");

			for (j = 0; j < 9; j++)
			{
				if (Board[i, j] == 0)
				{
					if (j % 3 == 0)
						Console.Write("+   ");
					else
						Console.Write("|   ");
					continue;
				}
				if (j % 3 == 0)
					Console.Write("+ ");
				else
					Console.Write("| ");

				if (wrongFields[i, j])
					Console.ForegroundColor = ConsoleColor.Red;

				Console.Write(Board[i, j] + " ");
				Console.ResetColor();
			}
			Console.WriteLine("+");
		}
		Console.WriteLine("+++++++++++++++++++++++++++++++++++++");
	}

	public bool[,] WrongFields()
	{
		int[,] row = new int[9, 10];
		int[,] col = new int[9, 10];
		int[,] box = new int[9, 10];
		bool[,] wrong = new bool[9, 9];

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				row[i, Board[i, j]]++;
				col[j, Board[i, j]]++;
				box[(i / 3) * 3 + j / 3, Board[i, j]]++;
			}
		}
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if (row[i, Board[i, j]] > 1 || col[j, Board[i, j]] > 1 || box[(i / 3) * 3 + j / 3, Board[i, j]] > 1)
					wrong[i, j] = true;
				else
					wrong[i, j] = false;
			}
		}
		return wrong;
	}

	// if score is 0 sudoku is solved, if higher score the sudoku solution is worse
	public int Score()
	{
		int count = 0;
		
		int[,] row = new int[9,10];
		int[,] col = new int[9, 10];
		int[,] box = new int[9,10];

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				row[i, Board[i, j]]++;
				col[j, Board[i, j]]++;
				box[(i / 3) * 3 + j / 3, Board[i, j]]++;
			}
		}
		for(int i = 0; i < 9; i++)
		{
			for (int j = 0; j <= 9; j++)
			{
				if (row[i, j] > 1)
					count+=row[i,j]-1;
				if (col[i, j] > 1)
					count += col[i, j] - 1;
				if (box[i, j] > 1)
					count += box[i, j] - 1;
			}
		}
		return count;
	}

	// it fill sudoku board with dna and returns new sudoku board
	public SudokuBoard Fit(DNA dna)
	{
		SudokuBoard board = new SudokuBoard(this);
		int index = 0;

		for(int i=0; i< 9; i++)
		{
			for(int j=0; j<9; j++)
			{
				if (board.Board[i, j] == 0)
				{
					board.Board[i, j] = dna.digits[index];
					index++;
				}
			}
		}

		return board;
	}
}

