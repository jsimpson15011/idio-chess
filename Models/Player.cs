using System.Collections.Generic;
using System;

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

        public ChessSquare[] FindMove(ChessBoard board)
        {
            List<ChessSquare[]> allMoves = FindAllMoves(board);
            if (allMoves.Count == 0)
            {
                return null;
            }

            int[] maxMoveIndexValue = {0, -999};
            int i = 0;

            foreach (ChessSquare[] move in allMoves)
            {
                ChessBoard tmpBoard = new ChessBoard(board);
                int moveValue = 0;
                ChessPiece playerPiece = move[0].Piece;
                ChessPiece capturedPiece = move[1].Piece;
                List<Threat> threatsOnMove =
                    Color == "white" ? move[1].UnderThreatFromBlack : move[1].UnderThreatFromWhite;
                List<Threat> threatsOnCurrentSquare =
                    Color == "white" ? move[0].UnderThreatFromBlack : move[0].UnderThreatFromWhite;
                int[] currentLocation = board.KeyToLocation(move[0].Key);
                int[] moveLocation = board.KeyToLocation(move[1].Key);
                
                
                tmpBoard.SetActiveSquare(tmpBoard.Board[currentLocation[0]][currentLocation[1]]);
                tmpBoard.Move(tmpBoard.Board[moveLocation[0]][moveLocation[1]]);
                tmpBoard.AddAllThreats();
                
                bool opponentIsInCheck = Color == "white" ? tmpBoard.IsKingInCheck("black") : tmpBoard.IsKingInCheck("white");

                if (capturedPiece != null)
                {
                    moveValue += capturedPiece.Value;
                }

                if (threatsOnMove?.Count > 0)
                {
                    moveValue -= playerPiece.Value;
                }

                if (threatsOnMove == null && (move[1].Key == "4d" || move[1].Key == "5d" || move[1].Key == "4e" ||
                                              move[1].Key == "5e"))
                {
                    moveValue += 2;
                }

                if (threatsOnCurrentSquare != null && threatsOnMove == null)
                {
                    moveValue += playerPiece.Value;
                }

                if (opponentIsInCheck && threatsOnMove == null)
                {
                    moveValue += 4;
                }


                /*if (playerPiece.Name == "pawn" && threatsOnMove == null)
                {
                    moveValue += 1;
                }*/

                if (moveValue > maxMoveIndexValue[1])
                {
                    maxMoveIndexValue[0] = i;
                    maxMoveIndexValue[1] = moveValue;
                }

                i++;
            }

            return allMoves[maxMoveIndexValue[0]];
        }
    }
}