using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using idiot_chess.Models;
using Microsoft.AspNetCore.Mvc;

namespace idiot_chess.Controllers
{
    [ApiController]
    [Route("chessboard")]
    public class ChessBoardController : ControllerBase
    {
        [HttpGet]
        public ChessBoard Get()
        {
            ChessBoard board = new ChessBoard();
            //return Enumerable.Range(1,1).Select(index => new ChessBoard()).ToArray();
            return board;
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public ActionResult<ChessBoard> UpdateBoard(ChessBoardDto body)
        {
            try
            {
                ChessBoard board = body.Board;
                ChessSquare currentSquare = body.CurrentSquare;

                if (board.PawnToUpgrade != null)
                {
                    board.SetSquareByKey(board.PawnToUpgrade.Key, board.PawnToUpgrade.Piece);
                    board.PawnToUpgrade = null;
                    board.SwitchPlayers();
                }

                if (currentSquare == null)
                {
                    return CreatedAtAction("UpdateBoard", board, board);
                }
                
                if (currentSquare.Piece != null && currentSquare.Piece.Color == board.ActivePlayer.Color)
                {
                    board.ClearAllSquareStatus();
                    board.SetActiveSquare(body.CurrentSquare);
                }
                
                var allMoves = board.ActivePlayer.FindAllMoves(board);
                if (allMoves.Count == 0)
                {
                    board.GameState.Player = board.ActivePlayer;
                    board.GameState.State = board.IsKingInCheck(board.ActivePlayer.Color) ? "Lose" : "Draw";
                    
                    return CreatedAtAction("UpdateBoard", board, board);
                }

                if (currentSquare.CanMoveTo)
                {
                    board.Move(currentSquare);
                    board.AddAllThreats();
                    if (board.PawnToUpgrade == null)
                    {
                        board.SwitchPlayers();
                    }
                }


                return CreatedAtAction("UpdateBoard", board, board);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}