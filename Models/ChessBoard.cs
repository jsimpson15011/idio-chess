using System;
using System.Collections.Generic;
using System.Linq;

namespace idiot_chess.Models
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
            Dictionary<string, int[]> squareLocations = new Dictionary<string, int[]>();

            for (int i = 0; i < initBoard.Length; i++)
            {
                initBoard[i] = new ChessSquare[8];
                for (int j = 0; j < initBoard[i].Length; j++)
                {
                    string key = (8 - i) + indexAlpha[j];
                    initBoard[i][j] = new ChessSquare(key);

                    squareLocations.Add(key, new[] {i, j});
                }

                _squareLocations = squareLocations;
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

        public ChessSquare ActiveSquare { get; set; }

        public Player ActivePlayer { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        private readonly Dictionary<string, int[]> _squareLocations = new Dictionary<string, int[]>();

        public void SetActiveSquare(ChessSquare activeSquare)
        {
            int[] indexesOfActivePiece = _squareLocations[activeSquare.Key];
            ChessSquare square = Board[indexesOfActivePiece[0]][indexesOfActivePiece[1]];

            if (square.Piece?.Color == ActivePlayer.Color)
            {
                List<int[]> possibleMoves = FindMoves(square);
                Board[indexesOfActivePiece[0]][indexesOfActivePiece[1]].IsActive = true;
                ActiveSquare = square;

                foreach (int[] move in possibleMoves.ToList())
                {
                    Board[move[0]][move[1]].CanMoveTo = true;
                }
            }
        }

        public List<int[]> FindMoves(ChessSquare square)
        {
            List<int[]> solution = new List<int[]>();
            ChessPiece piece = square.Piece;
            int[] pieceLocation = _squareLocations[square.Key];

            if (piece.Name == "pawn")
            {
                int pawnMovementDirection = ActivePlayer.Color == Player1.Color ? -1 : 1;

                if (Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1]].Piece == null)
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1]});
                    if (Board[pieceLocation[0] + 2 * pawnMovementDirection][pieceLocation[1]].Piece == null &&
                        !piece.HasMoved)
                    {
                        solution.Add(new[] {pieceLocation[0] + 2 * pawnMovementDirection, pieceLocation[1]});
                    }
                }

                //The following adds the pawns attack moves
                if (pieceLocation[0] < 7 &&
                    pieceLocation[1] > 0 &&
                    Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1].Piece != null &&
                    Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1]?.Piece.Color !=
                    ActivePlayer.Color)
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1] - 1});
                }

                if (pieceLocation[0] < 7 &&
                    pieceLocation[1] < 7 &&
                    Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1].Piece != null &&
                    Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1]?.Piece.Color !=
                    ActivePlayer.Color)
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1] + 1});
                }
            }

            if (piece.Name == "bishop")
            {
                FindDiagonals(pieceLocation, solution, new[] {-1, -1});
                FindDiagonals(pieceLocation, solution, new []{-1, 1});
                FindDiagonals(pieceLocation, solution, new []{1, -1});
                FindDiagonals(pieceLocation, solution, new []{1, 1});
            }

            foreach (int[] location in solution.ToList())
            {
                if (Board[location[0]][location[1]].Piece?.Color ==
                    ActivePlayer.Color) //remove possible moves if they land on your own piece
                {
                    solution.Remove(location);
                }
            }

            return solution;
        }

        public void Move(ChessSquare currentSquare)
        {
            int[] currentSquareLocation = _squareLocations[currentSquare.Key];
            ActiveSquare.Piece.HasMoved = true;
            Board[currentSquareLocation[0]][currentSquareLocation[1]].Piece = ActiveSquare.Piece;
            ChessSquare activeSquareInBoard = FindActiveSquare();

            activeSquareInBoard.Piece = null;
            activeSquareInBoard.IsActive = false;
            ActiveSquare = null;
            ClearAllSquareStatus();
        }

        public ChessSquare FindActiveSquare()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    if (Board[i][j].IsActive)
                    {
                        return Board[i][j];
                    }
                }
            }

            return new ChessSquare("xx");
        }

        private void FindDiagonals(IReadOnlyList<int> pieceLocation, ICollection<int[]> solution, int[] direction)
        {
            int[] possibleMove = {pieceLocation[0] + direction[0], pieceLocation[1] + direction[1]};


            if (
                (direction[0] == 1 || pieceLocation[0] > 0) &&
                (direction[0] == -1 || pieceLocation[0] < Board.Length - 1) &&
                (direction[1] == 1 || pieceLocation[1] > 0) &&
                (direction[1] == -1 || pieceLocation[1] < Board.Length - 1)
            )
            {
                ChessSquare squareToCheck = Board[possibleMove[0]][possibleMove[1]];

                while ((direction[0] == 1 || possibleMove[0] >= 0) &&
                       (direction[0] == -1 || possibleMove[0] <= Board.Length - 1) &&
                       (direction[1] == 1 || possibleMove[1] >= 0) &&
                       (direction[1] == -1 || possibleMove[1] <= Board.Length - 1) &&
                       (squareToCheck.Piece == null || squareToCheck.Piece.Color != ActivePlayer.Color))
                {
                    if (squareToCheck.Piece == null || squareToCheck.Piece.Color != ActivePlayer.Color)
                    {
                        solution.Add(new int[] {possibleMove[0], possibleMove[1]});
                    }

                    if (squareToCheck.Piece != null && squareToCheck.Piece?.Color != ActivePlayer.Color)
                    {
                        return;
                    }

                    possibleMove[0] += direction[0];
                    possibleMove[1] += direction[1];
                    
                    if ((direction[0] == 1 || possibleMove[0] >= 0) &&
                        (direction[0] == -1 || possibleMove[0] <= Board.Length - 1) &&
                        (direction[1] == 1 || possibleMove[1] >= 0) &&
                        (direction[1] == -1 || possibleMove[1] <= Board.Length - 1))
                    {
                        squareToCheck = Board[possibleMove[0]][possibleMove[1]];
                    }
                }
            }
        }

        public void ClearAllSquareStatus()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    Board[i][j].IsActive = false;
                    Board[i][j].CanMoveTo = false;
                }
            }
        }
    }
}