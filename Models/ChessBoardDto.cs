namespace idiot_chess.Models
{
    public class ChessBoardDto
    {
        public ChessBoardDto()
        {
            
        }

        public ChessBoard Board { get; set; }

        public ChessSquare CurrentSquare { get; set; }
    }
}