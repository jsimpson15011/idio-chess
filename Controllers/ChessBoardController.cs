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
                    board.ActivePlayer = board.ActivePlayer.Color == board.Player1.Color ? board.Player2 : board.Player1;
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

                if (currentSquare.CanMoveTo)
                {
                    board.Move(currentSquare);
                    board.AddAllThreats();
                    if (board.PawnToUpgrade == null)
                    {
                        board.ActivePlayer = board.ActivePlayer.Color == board.Player1.Color ? board.Player2 : board.Player1;
                    }
                }


                /*if (board.ActivePlayer.Color == currentSquare.Piece.Color)
                {
                    
                }*/



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