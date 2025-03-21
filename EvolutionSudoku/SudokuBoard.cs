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


	public void Print()
	{
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
					Console.Write("+ " + Board[i, j] + " ");
				else
					Console.Write("| " + Board[i, j] + " ");
			}
			Console.WriteLine("+");
		}
		Console.WriteLine("+++++++++++++++++++++++++++++++++++++");
	}


	// if score is 0 sudoku is solved, if higher score the sudoku solution is worse
	public int Score()
	{
		return 81;
	}

	// it fill sudoku board with dna and returns new sudoku board
	public SudokuBoard Fit(DNA dna)
	{
		return new SudokuBoard();
	}
}

