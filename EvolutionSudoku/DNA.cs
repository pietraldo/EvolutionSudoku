using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionSudoku;
public class DNA
{
	public int[] digits;

	public DNA(int[] digits)
	{ 
		this.digits = digits; 
	}

	public void Print()
	{
		for(int i=0; i<digits.Length; i++)
		{
			Console.Write(digits[i]);
		}
        Console.WriteLine();
    }

	public void SetRandomDNA()
	{
		for (int i = 0; i < digits.Length; i++)
		{
			digits[i] = new Random().Next(1, 10);
		}
	}
}

