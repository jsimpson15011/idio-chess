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
            Dictionary<int, string> indexAlpha = new Dictionary<int, string>
            {
                {0, "a"},
                {1, "b"},
                {2, "c"},
                {3, "d"},
                {4, "e"},
                {5, "f"},
                {6, "g"},
                {7, "h"}
            };
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

        public ChessBoard(ChessBoard chessBoard)
        {
            ChessSquare[][] newBoard = new ChessSquare[8][];

            for (int i = 0; i < newBoard.Length; i++)
            {
                newBoard[i] = new ChessSquare[8];

                for (int j = 0; j < newBoard[i].Length; j++)
                {
                    ChessSquare oldSquare = chessBoard.Board[i][j];

                    if (oldSquare.Piece == null)
                    {
                        newBoard[i][j] = new ChessSquare(oldSquare.Key);
                    }
                    else
                    {
                        newBoard[i][j] = new ChessSquare(oldSquare.Key,
                            new ChessPiece(oldSquare.Piece.Color, oldSquare.Piece.Name, oldSquare.Piece.HasMoved));
                    }
                }
            }

            Dictionary<int, string> indexAlpha = new Dictionary<int, string>
            {
                {0, "a"},
                {1, "b"},
                {2, "c"},
                {3, "d"},
                {4, "e"},
                {5, "f"},
                {6, "g"},
                {7, "h"}
            };

            Dictionary<string, int[]> squareLocations = new Dictionary<string, int[]>();

            for (int i = 0; i < newBoard.Length; i++)
            {
                for (int j = 0; j < newBoard[i].Length; j++)
                {
                    string key = (8 - i) + indexAlpha[j];

                    squareLocations.Add(key, new[] {i, j});
                }
            }

            _squareLocations = squareLocations;

            Board = newBoard;
            ActivePlayer = chessBoard.ActivePlayer;
            ActiveSquare = chessBoard.ActiveSquare;
            Player1 = chessBoard.Player1;
            Player2 = chessBoard.Player2;
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
                    if (Board[move[0]][move[1]].Piece?.Color ==
                        activeSquare.Piece.Color) //remove possible moves if they land on the same color piece
                    {
                        possibleMoves.Remove(move);
                    }
                }

                foreach (int[] move in possibleMoves.ToList())
                {
                    Board[move[0]][move[1]].CanMoveTo = true;
                }
            }
        }

        public List<int[]> FindMoves(ChessSquare square, bool isThreatMoves = false)
        {
            List<int[]> solution = new List<int[]>();
            ChessPiece piece = square.Piece;
            int[] pieceLocation = _squareLocations[square.Key];

            if (piece == null)
            {
                return solution;
            }

            if (piece.Name == "pawn")
            {
                int pawnMovementDirection = piece.Color == Player1.Color ? -1 : 1;

                if (Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1]].Piece == null)
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1]});
                    if (!piece.HasMoved &&
                        Board[pieceLocation[0] + 2 * pawnMovementDirection][pieceLocation[1]].Piece == null)
                    {
                        solution.Add(new[] {pieceLocation[0] + 2 * pawnMovementDirection, pieceLocation[1]});
                    }
                }

                //The following adds the pawns attack moves
                if (pieceLocation[0] < 7 && pieceLocation[1] > 0 &&
                    (Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1].Piece != null &&
                     Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1]?.Piece.Color !=
                     piece.Color ||
                     Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1].EnPassantPieceSquare !=
                     null &&
                     Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] - 1].EnPassantPieceSquare.Piece
                         ?.Color != piece.Color)
                )
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1] - 1});
                }

                if (pieceLocation[0] < 7 && pieceLocation[1] < 7 &&
                    (Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1].Piece != null &&
                     Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1]?.Piece.Color !=
                     piece.Color
                     || Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1].EnPassantPieceSquare !=
                     null &&
                     Board[pieceLocation[0] + pawnMovementDirection][pieceLocation[1] + 1].EnPassantPieceSquare.Piece
                         ?.Color != piece.Color
                    )
                )
                {
                    solution.Add(new[] {pieceLocation[0] + pawnMovementDirection, pieceLocation[1] + 1});
                }
            }

            if (piece.Name == "bishop" || piece.Name == "queen")
            {
                FindMovesFromDirection(pieceLocation, solution, new[] {-1, -1}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {-1, 1}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {1, -1}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {1, 1}, isThreatMoves);
            }

            if (piece.Name == "rook" || piece.Name == "queen")
            {
                FindMovesFromDirection(pieceLocation, solution, new[] {-1, 0}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {1, 0}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {0, -1}, isThreatMoves);
                FindMovesFromDirection(pieceLocation, solution, new[] {0, 1}, isThreatMoves);
            }

            if (piece.Name == "knight")
            {
                if (pieceLocation[0] - 2 >= 0 && pieceLocation[1] + 1 <= Board.Length - 1)
                {
                    solution.Add(new[] {pieceLocation[0] - 2, pieceLocation[1] + 1});
                }

                if (pieceLocation[0] - 2 >= 0 && pieceLocation[1] - 1 >= 0)
                {
                    solution.Add(new[] {pieceLocation[0] - 2, pieceLocation[1] - 1});
                }

                if (pieceLocation[0] + 2 <= Board.Length - 1 && pieceLocation[1] + 1 <= Board.Length - 1)
                {
                    solution.Add(new[] {pieceLocation[0] + 2, pieceLocation[1] + 1});
                }

                if (pieceLocation[0] + 2 <= Board.Length - 1 && pieceLocation[1] - 1 >= 0)
                {
                    solution.Add(new[] {pieceLocation[0] + 2, pieceLocation[1] - 1});
                }

                if (pieceLocation[0] - 1 >= 0 && pieceLocation[1] + 2 <= Board.Length - 1)
                {
                    solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1] + 2});
                }

                if (pieceLocation[0] - 1 >= 0 && pieceLocation[1] - 2 >= 0)
                {
                    solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1] - 2});
                }

                if (pieceLocation[0] + 1 <= Board.Length - 1 && pieceLocation[1] + 2 <= Board.Length - 1)
                {
                    solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1] + 2});
                }

                if (pieceLocation[0] + 1 <= Board.Length - 1 && pieceLocation[1] - 2 >= 0)
                {
                    solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1] - 2});
                }
            }

            if (piece.Name == "king")
            {
                //check for castling
                if (piece.HasMoved == false)
                {
                    if ((piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1]].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1]].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] - 1].Piece == null &&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] - 1].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] - 1].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] - 2].Piece == null &&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] - 2].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] - 2].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] - 3].Piece == null &&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] - 3].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] - 3].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] - 4].Piece?.HasMoved == false /*&&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] - 4].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] - 4].UnderThreatFromWhite == null)*/
                    )
                    {
                        int[] castlingMove = {pieceLocation[0], pieceLocation[1] - 2};
                        solution.Add(castlingMove);

                        Board[pieceLocation[0]][pieceLocation[1] - 2].SquareWithRookToCastle =
                            Board[pieceLocation[0]][pieceLocation[1] - 4];
                    }

                    if ((piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1]].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1]].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] + 1].Piece == null &&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] + 1].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] + 1].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] + 2].Piece == null &&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] + 2].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] + 2].UnderThreatFromWhite == null) &&
                        Board[pieceLocation[0]][pieceLocation[1] + 3].Piece?.HasMoved == false /*&&
                        (piece.Color == "white"
                            ? Board[pieceLocation[0]][pieceLocation[1] + 3].UnderThreatFromBlack == null
                            : Board[pieceLocation[0]][pieceLocation[1] + 3].UnderThreatFromWhite == null)*/
                    )
                    {
                        int[] castlingMove = {pieceLocation[0], pieceLocation[1] + 2};
                        solution.Add(castlingMove);

                        Board[pieceLocation[0]][pieceLocation[1] + 2].SquareWithRookToCastle =
                            Board[pieceLocation[0]][pieceLocation[1] + 3];
                    }
                }

                if (pieceLocation[0] - 1 >= 0)
                {
                    if (pieceLocation[1] - 1 >= 0)
                    {
                        solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1] - 1});
                        solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1]});
                        solution.Add(new[] {pieceLocation[0], pieceLocation[1] - 1});
                    }

                    if (pieceLocation[1] + 1 <= Board.Length - 1)
                    {
                        solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1] + 1});
                        solution.Add(new[] {pieceLocation[0] - 1, pieceLocation[1]});
                        solution.Add(new[] {pieceLocation[0], pieceLocation[1] + 1});
                    }
                }

                if (pieceLocation[0] + 1 <= Board.Length - 1)
                {
                    if (pieceLocation[1] - 1 >= 0)
                    {
                        solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1] - 1});
                        solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1]});
                        solution.Add(new[] {pieceLocation[0], pieceLocation[1] - 1});
                    }

                    if (pieceLocation[1] + 1 <= Board.Length - 1)
                    {
                        solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1] + 1});
                        solution.Add(new[] {pieceLocation[0] + 1, pieceLocation[1]});
                        solution.Add(new[] {pieceLocation[0], pieceLocation[1] + 1});
                    }
                }
            }

            foreach (int[] location in solution.ToList())
            {
                if (!isThreatMoves)
                {
                    ChessBoard tmpBoard = new ChessBoard(this);

                    tmpBoard.ActiveSquare = tmpBoard.Board[pieceLocation[0]][pieceLocation[1]];

                    tmpBoard.Move(tmpBoard.Board[location[0]][location[1]]);

                    tmpBoard.AddAllThreats();

                    bool movePutsKingInCheck = tmpBoard.IsKingInCheck(piece.Color);

                    //remove moves that put king into check
                    if (movePutsKingInCheck)
                    {
                        solution.Remove(location);
                    }
                }
            }

            return solution;
        }

        public void Move(ChessSquare currentSquare)
        {
            int[] currentSquareLocation = _squareLocations[currentSquare.Key];
            int[] activeSquareLocation = _squareLocations[ActiveSquare.Key];
            ActiveSquare.Piece.HasMoved = true;
            ChessSquare activeSquareInBoard = Board[activeSquareLocation[0]][activeSquareLocation[1]];
            //Check if the king is castling
            if (ActiveSquare.Piece.Name == "king" && currentSquare.SquareWithRookToCastle != null)
            {
                int kingMovementDirection = currentSquareLocation[1] - activeSquareLocation[1] > 0 ? -1 : 1;

                int[] rookLocation = _squareLocations[currentSquare.SquareWithRookToCastle.Key];
                ChessPiece rookToMove = Board[rookLocation[0]][rookLocation[1]].Piece;
                rookToMove.HasMoved = true;
                Board[currentSquareLocation[0]][currentSquareLocation[1]].Piece = ActiveSquare.Piece;
                Board[currentSquareLocation[0]][currentSquareLocation[1] + kingMovementDirection].Piece = rookToMove;
                Board[rookLocation[0]][rookLocation[1]].Piece = null;
            }
            else
            {
                Board[currentSquareLocation[0]][currentSquareLocation[1]].Piece = ActiveSquare.Piece;
            }

            if (ActiveSquare.Piece.Name == "pawn" && currentSquare.EnPassantPieceSquare != null)
            {
                int[] enPassantPieceLocation = _squareLocations[currentSquare.EnPassantPieceSquare.Key];
                Board[enPassantPieceLocation[0]][enPassantPieceLocation[1]].Piece = null;
            }


            //keep track of square that is enpassant
            if (ActiveSquare.Piece.Name == "pawn" &&
                Math.Abs(currentSquareLocation[0] - activeSquareLocation[0]) > 1)
            {
                ClearAllSquareStatus();
                ClearEnPassantSquares();
                int pawnDirection = ActiveSquare.Piece.Color == Player1.Color ? 1 : -1;

                Board[currentSquareLocation[0] + pawnDirection][currentSquareLocation[1]].EnPassantPieceSquare =
                    Board[currentSquareLocation[0]][currentSquareLocation[1]];
                activeSquareInBoard.Piece = null;
                activeSquareInBoard.IsActive = false;
                ActiveSquare = null;
            }
            else
            {
                activeSquareInBoard.Piece = null;
                activeSquareInBoard.IsActive = false;
                ActiveSquare = null;
                ClearAllSquareStatus();
                ClearEnPassantSquares();
            }
        }

        public Dictionary<string, List<Threat>> FindThreats(ChessSquare currentSquare)
        {
            Dictionary<string, List<Threat>> solution = new Dictionary<string, List<Threat>>
            {
                {"white", new List<Threat>()}, {"black", new List<Threat>()}
            };

            int[] currentSquareLocation = _squareLocations[currentSquare.Key];

            foreach (var row in Board)
            {
                foreach (var currentThreat in row)
                {
                    if (currentThreat.Piece != null)
                    {
                        if (currentThreat.Piece?.Name ==
                            "pawn") //if the piece is a pawn it's attacks may not be it's moves
                        {
                            int pawnMovementDirection = currentThreat.Piece?.Color == Player1.Color ? -1 : 1;
                            int[] currentThreatLocation = _squareLocations[currentThreat.Key];
                            List<int[]> currentThreatMoves = new List<int[]>();
                            currentThreatMoves.Add(new[]
                                {currentThreatLocation[0] + pawnMovementDirection, currentThreatLocation[1] - 1});
                            currentThreatMoves.Add(new[]
                                {currentThreatLocation[0] + pawnMovementDirection, currentThreatLocation[1] + 1});

                            foreach (var currentThreatMove in currentThreatMoves)
                            {
                                if (currentThreatMove.SequenceEqual(currentSquareLocation))
                                {
                                    ChessSquare threatSquare =
                                        Board[currentThreatLocation[0]][currentThreatLocation[1]];

                                    Threat threat = new Threat(threatSquare.Key,
                                        new ChessPiece(threatSquare.Piece.Color, threatSquare.Piece.Name));
                                    solution[currentThreat.Piece?.Color == "white" ? "white" : "black"]
                                        .Add(threat);
                                }
                            }
                        }
                        else
                        {
                            int[] currentThreatLocation = _squareLocations[currentThreat.Key];
                            List<int[]> currentThreatMoves = FindMoves(currentThreat, true);

                            foreach (int[] currentThreatMove in currentThreatMoves)
                            {
                                if (currentThreatMove.SequenceEqual(currentSquareLocation))
                                {
                                    ChessSquare threatSquare =
                                        Board[currentThreatLocation[0]][currentThreatLocation[1]];

                                    Threat threat = new Threat(threatSquare.Key,
                                        new ChessPiece(threatSquare.Piece.Color, threatSquare.Piece.Name));
                                    solution[currentThreat.Piece?.Color == "white" ? "white" : "black"]
                                        .Add(threat);
                                }
                            }
                        }
                    }
                }
            }

            solution["white"] = solution["white"].Count > 0 ? solution["white"] : null;
            solution["black"] = solution["black"].Count > 0 ? solution["black"] : null;

            return solution;
        }

        private void FindMovesFromDirection(IReadOnlyList<int> pieceLocation, ICollection<int[]> solution,
            int[] direction, bool isThreatMoves = false)
        {
            ChessSquare currentSquare = Board[pieceLocation[0]][pieceLocation[1]];
            ChessPiece piece = currentSquare.Piece;
            int[] possibleMove = {pieceLocation[0] + direction[0], pieceLocation[1] + direction[1]};

            if ((direction[0] != 1 && direction[0] != 0 && pieceLocation[0] <= 0) ||
                (direction[0] != -1 && direction[0] != 0 && pieceLocation[0] >= Board.Length - 1) ||
                (direction[1] != 1 && direction[1] != 0 && pieceLocation[1] <= 0) ||
                (direction[1] != -1 && direction[1] != 0 && pieceLocation[1] >= Board.Length - 1))
            {
                return;
            }

            ChessSquare squareToCheck = Board[possibleMove[0]][possibleMove[1]];

            while ((direction[0] == 1 || direction[0] == 0 || possibleMove[0] >= 0) &&
                   (direction[0] == -1 || direction[0] == 0 || possibleMove[0] <= Board.Length - 1) &&
                   (direction[1] == 1 || direction[1] == 0 || possibleMove[1] >= 0) &&
                   (direction[1] == -1 || direction[1] == 0 || possibleMove[1] <= Board.Length - 1)
            )
            {
                if (squareToCheck.Piece == null)
                {
                    solution.Add(new int[] {possibleMove[0], possibleMove[1]});
                }

                else if (squareToCheck.Piece != null)
                {
                    solution.Add(new int[] {possibleMove[0], possibleMove[1]});
                    return;
                }

                possibleMove[0] += direction[0];
                possibleMove[1] += direction[1];

                if ((direction[0] == 1 || direction[0] == 0 || possibleMove[0] >= 0) &&
                    (direction[0] == -1 || direction[0] == 0 || possibleMove[0] <= Board.Length - 1) &&
                    (direction[1] == 1 || direction[1] == 0 || possibleMove[1] >= 0) &&
                    (direction[1] == -1 || direction[1] == 0 || possibleMove[1] <= Board.Length - 1))
                {
                    squareToCheck = Board[possibleMove[0]][possibleMove[1]];
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
                    Board[i][j].SquareWithRookToCastle = null;
                }
            }
        }

        public void ClearEnPassantSquares()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board[i].Length; j++)
                {
                    Board[i][j].EnPassantPieceSquare = null;
                }
            }
        }

        public void AddAllThreats()
        {
            foreach (ChessSquare[] row in Board)
            {
                foreach (ChessSquare square in row)
                {
                    int[] squareLocation = _squareLocations[square.Key];
                    List<Threat> whiteThreats = FindThreats(square)["white"];
                    List<Threat> blackThreats = FindThreats(square)["black"];
                    Board[squareLocation[0]][squareLocation[1]].UnderThreatFromBlack = blackThreats;
                    Board[squareLocation[0]][squareLocation[1]].UnderThreatFromWhite = whiteThreats;
                }
            }
        }

        public bool IsKingInCheck(string color)
        {
            ChessSquare kingSquare = null;
            foreach (ChessSquare[] row in Board)
            {
                foreach (ChessSquare square in row)
                {
                    if (square.Piece?.Name == "king" && square.Piece?.Color == color)
                    {
                        kingSquare = square;
                    }
                }
            }

            if (color == "white")
            {
                return kingSquare?.UnderThreatFromBlack != null;
            }

            return kingSquare?.UnderThreatFromWhite != null;
        }
    }
}