using System;
using System.Collections.Generic;
using System.Linq;

namespace Eight_Queens
{
    public class EightQueens
    {
        List<ChessBoard> chessBoards = new List<ChessBoard>();
        Random random = new Random();

        int failsInARow;

        public void ShowSolutions()
        {
            string boardOutput;

            Console.WriteLine("Number of solutions found: " + chessBoards.Count);
            Console.WriteLine();

            foreach (var chessBoard in chessBoards)
            {
                Console.WriteLine("|A|B|C|D|E|F|G|H|");

                boardOutput = "|";
                for (int i = 0; i < chessBoard.Board.Length; i++)
                {
                    boardOutput = boardOutput + chessBoard.Board[i] + "|";
                }
                Console.WriteLine(boardOutput);
                Console.WriteLine();
            }
        }

        public void CalculateAllSolutions()
        {
            // There are 92 possible solutions for this problem
            // Maximum 1000 fails in a row allowed
            while (chessBoards.Count < 92 && failsInARow < 1000)
            {
                ChessBoard newChessBoard = new ChessBoard();

                newChessBoard = CalculateSolution(newChessBoard);

                //Check if board is completed
                if (newChessBoard.Board[7] != 0)
                {
                    AddBoardToBoardList(newChessBoard);
                }
                
            }
        }

        private void AddBoardToBoardList(ChessBoard newChessBoard)
        {
            bool alreadyExisting = false;

            foreach (var chessBoard in chessBoards)
            {
                if (chessBoard.Board.SequenceEqual(newChessBoard.Board))
                {
                    alreadyExisting = true;
                    break;
                }
            }

            if (!alreadyExisting || chessBoards.Count == 0)
            {
                chessBoards.Add(newChessBoard);
                failsInARow = 0;
            }
            else
            {
                failsInARow++;
            }
        }

        private ChessBoard CalculateSolution(ChessBoard newChessBoard)
        {
            int fails = 0;
            int newNumber;
            int randomIndex;
            List<int> availableNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            for (int i = 0; i < newChessBoard.Board.Length; i++)
            {
                //Max 1000 fails in a row allowed
                while (newChessBoard.Board[i] == 0 && fails < 1000)
                {
                    randomIndex = random.Next(availableNumbers.Count);
                    newNumber = availableNumbers[randomIndex];
                    if (!newChessBoard.CheckCollision(newNumber, i))
                    {
                        newChessBoard.Board[i] = newNumber;
                        availableNumbers.RemoveAt(randomIndex);
                        fails = 0;
                    }
                    else
                    {
                        fails++;
                    }
                }
            }

            return newChessBoard;
        }
    }
}
