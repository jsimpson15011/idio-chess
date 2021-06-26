using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace idiot_chess.Controllers
{
    [ApiController]
    [Route("chessboard")]
    public class ChessBoardController : ControllerBase
    {
        [HttpGet]
        public char[][] Get()
        {
            ChessBoard board = new ChessBoard();
            //return Enumerable.Range(1,1).Select(index => new ChessBoard()).ToArray();
            return board.Board;
        }
    }
}