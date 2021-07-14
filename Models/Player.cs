using System.Collections.Generic;

namespace idiot_chess.Models
{
    public class Player
    {
        public string Color { get; set; }

        public bool IsComputer { get; set; }
        
        public bool IsActive { get; set; }
        
        public List<ChessSquare[]> FindAllMoves(ChessBoard board)
        {
            List<ChessSquare[]> solution = new List<ChessSquare[]>();

            foreach (ChessSquare[] row in board.Board)
            {
                foreach (ChessSquare square in row)
                {
                    if (square.Piece?.Color == Color)
                    {
                        List<int[]> moves = board.FindMoves(square);
                        foreach (int[] move in moves)
                        {
                            ChessSquare[] activeSquareCurrentSquare = {square, board.Board[move[0]][move[1]]};
                            solution.Add(activeSquareCurrentSquare);
                        }
                    }
                }
            }
            
            return solution;
        }
    }
}