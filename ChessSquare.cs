using idiot_chess.ClientApp;

namespace idiot_chess
{
    public class ChessSquare
    {
        public ChessSquare(string key, ChessPiece piece)
        {
            Piece = piece;
            Key = key;
        }

        public ChessSquare(string key)
        {
            Key = key;
            Piece = null;
        }

        public string Key { get; }
        public ChessPiece Piece { get; set; }
    }
}