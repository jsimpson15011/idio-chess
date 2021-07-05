namespace idiot_chess.Models
{
    public class ChessSquare
    {
        public ChessSquare()
        {}
        public ChessSquare(string key, ChessPiece piece)
        {
            Piece = piece;
            Key = key;
            IsActive = false;
            CanMoveTo = false;
        }

        public ChessSquare(string key)
        {
            Key = key;
            Piece = null;
            IsActive = false;
            CanMoveTo = false;
        }

        public string Key { get; set; }
        public ChessPiece Piece { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool CanMoveTo { get; set; }
    }
}