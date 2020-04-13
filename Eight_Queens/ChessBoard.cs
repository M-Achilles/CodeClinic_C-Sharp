using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Eight_Queens
{
    class ChessBoard
    {
        public int[] Board { get; set; } = new int[8];

        /// <summary>
        /// Checks if a new queen, that whould be added to the array,
        /// collides with a existing queen
        /// </summary>
        /// <param name="newNumber"></param>
        /// <returns></returns>
        public bool CheckCollision(int newQueen, int column)
        {
            // True: There is a queen in the same row
            if (Board.Contains(newQueen))
            {
                return true;
            }

            // Check diagonal collision
            for (int i = 0; i < Board.Length; i++)
            {
                //Iterate over all columns with a queen
                if (Board[i] != 0)
                {
                    if (Math.Abs(i-column) == Math.Abs(Board[i] - newQueen))
                    {
                        return true;
                    }
                }
                // Else break out of the loop
                else
                {
                    break;
                }
            }
            return false;
        }
    }
}
