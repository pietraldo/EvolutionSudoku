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

	public void LoadFromFile(string path)
	{
		return;
	}

	public void Print()
	{
		return;
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

