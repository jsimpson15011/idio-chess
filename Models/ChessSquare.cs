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
            UnderThreatFrom = null;
            EnPassantPiece = null;
            RookToCastle = null;
        }

        public ChessSquare(string key)
        {
            Key = key;
            Piece = null;
            IsActive = false;
            CanMoveTo = false;
            UnderThreatFrom = null;
            EnPassantPiece = null;
            RookToCastle = null;
        }

        public string Key { get; set; }
        public ChessPiece Piece { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool CanMoveTo { get; set; }
        
        public ChessPiece UnderThreatFrom { get; set; }
        
        public ChessPiece EnPassantPiece { get; set; }
        
        public ChessPiece RookToCastle { get; set; }
    }
}