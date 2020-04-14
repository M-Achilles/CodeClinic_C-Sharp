using System;

namespace Eight_Queens
{
    class Program
    {
        static void Main(string[] args)
        {
            EightQueens eightQueens = new EightQueens();
            eightQueens.CalculateAllSolutions();

            eightQueens.ShowSolutions();

            Console.ReadLine();
        }
    }
}
