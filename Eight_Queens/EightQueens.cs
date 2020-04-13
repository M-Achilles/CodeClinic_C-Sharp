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

                AddBoardToBoardList(newChessBoard);
            }
        }

        private void AddBoardToBoardList(ChessBoard newChessBoard)
        {
            bool alreadyExisting = false;
            if (chessBoards.Count == 0)
            {
                chessBoards.Add(newChessBoard);
            }
            else
            {
                foreach (var chessBoard in chessBoards)
                {
                    if (chessBoard.Board.SequenceEqual(newChessBoard.Board))
                    {
                        alreadyExisting = true;
                        break;
                    }
                }
            }

            if (!alreadyExisting)
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
            newChessBoard.Board[0] = random.Next(1, 8);
            int newNumber;

            for (int i = 1; i < newChessBoard.Board.Length; i++)
            {
                while (newChessBoard.Board[i] == 0)
                {
                    newNumber = random.Next(1, 9);
                    if (!newChessBoard.CheckCollision(newNumber, i))
                    {
                        newChessBoard.Board[i] = newNumber;
                    }
                }
            }

            return newChessBoard;
        }
    }
}
