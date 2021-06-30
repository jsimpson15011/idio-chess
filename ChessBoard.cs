using System.Collections.Generic;
using idiot_chess.ClientApp;

namespace idiot_chess
{
    public class ChessBoard
    {
        public ChessBoard()
        {
            ChessSquare[][] initBoard = new ChessSquare[8][];
            Dictionary<int, string> indexAlpha = new Dictionary<int, string>();
            indexAlpha.Add(0, "a");
            indexAlpha.Add(1, "b");
            indexAlpha.Add(2, "c");
            indexAlpha.Add(3, "d");
            indexAlpha.Add(4, "e");
            indexAlpha.Add(5, "f");
            indexAlpha.Add(6, "g");
            indexAlpha.Add(7, "h");

            for (int i = 0; i < initBoard.Length; i++)
            {
                initBoard[i] = new ChessSquare[8];
                for (int j = 0; j < initBoard[i].Length; j++)
                {
                    string key = (8 - i) + indexAlpha[j];
                    initBoard[i][j] = new ChessSquare(key);
                }
            }

            initBoard[0][0] = new ChessSquare("8a", new ChessPiece("black", "rook"));
            initBoard[0][1] = new ChessSquare("8b", new ChessPiece("black", "knight"));
            initBoard[0][2] = new ChessSquare("8c", new ChessPiece("black", "bishop"));
            initBoard[0][3] = new ChessSquare("8d", new ChessPiece("black", "queen"));
            initBoard[0][4] = new ChessSquare("8e", new ChessPiece("black", "king"));
            initBoard[0][5] = new ChessSquare("8f", new ChessPiece("black", "bishop"));
            initBoard[0][6] = new ChessSquare("8g", new ChessPiece("black", "knight"));
            initBoard[0][7] = new ChessSquare("8h", new ChessPiece("black", "rook"));
            
            initBoard[1][0] = new ChessSquare("7a", new ChessPiece("black", "pawn"));
            initBoard[1][1] = new ChessSquare("7b", new ChessPiece("black", "pawn"));
            initBoard[1][2] = new ChessSquare("7c", new ChessPiece("black", "pawn"));
            initBoard[1][3] = new ChessSquare("7d", new ChessPiece("black", "pawn"));
            initBoard[1][4] = new ChessSquare("7e", new ChessPiece("black", "pawn"));
            initBoard[1][5] = new ChessSquare("7f", new ChessPiece("black", "pawn"));
            initBoard[1][6] = new ChessSquare("7g", new ChessPiece("black", "pawn"));
            initBoard[1][7] = new ChessSquare("7h", new ChessPiece("black", "pawn"));
            
            initBoard[7][0] = new ChessSquare("1a", new ChessPiece("white", "rook"));
            initBoard[7][1] = new ChessSquare("1b", new ChessPiece("white", "knight"));
            initBoard[7][2] = new ChessSquare("1c", new ChessPiece("white", "bishop"));
            initBoard[7][3] = new ChessSquare("1d", new ChessPiece("white", "queen"));
            initBoard[7][4] = new ChessSquare("1e", new ChessPiece("white", "king"));
            initBoard[7][5] = new ChessSquare("1f", new ChessPiece("white", "bishop"));
            initBoard[7][6] = new ChessSquare("1g", new ChessPiece("white", "knight"));
            initBoard[7][7] = new ChessSquare("1h", new ChessPiece("white", "rook"));
            
            initBoard[6][0] = new ChessSquare("2a", new ChessPiece("white", "pawn"));
            initBoard[6][1] = new ChessSquare("2b", new ChessPiece("white", "pawn"));
            initBoard[6][2] = new ChessSquare("2c", new ChessPiece("white", "pawn"));
            initBoard[6][3] = new ChessSquare("2d", new ChessPiece("white", "pawn"));
            initBoard[6][4] = new ChessSquare("2e", new ChessPiece("white", "pawn"));
            initBoard[6][5] = new ChessSquare("2f", new ChessPiece("white", "pawn"));
            initBoard[6][6] = new ChessSquare("2g", new ChessPiece("white", "pawn"));
            initBoard[6][7] = new ChessSquare("2h", new ChessPiece("white", "pawn"));

            Board = initBoard;

        }
        
        public ChessBoard(ChessSquare[][] board)
        {
            Board = board;
        }
        
        public ChessSquare[][] Board { get; set; }
    }
}