using System.Collections.Generic;

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
            UnderThreatFromWhite = null;
            UnderThreatFromBlack = null;
            EnPassantPieceSquare = null;
            SquareWithRookToCastle = null;
        }

        public ChessSquare(string key)
        {
            Key = key;
            Piece = null;
            IsActive = false;
            CanMoveTo = false;
            UnderThreatFromWhite = null;
            UnderThreatFromBlack = null;
            EnPassantPieceSquare = null;
            SquareWithRookToCastle = null;
        }

        public string Key { get; set; }
        public ChessPiece Piece { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool CanMoveTo { get; set; }
        
        public List<Threat> UnderThreatFromWhite { get; set; }
        
        public List<Threat> UnderThreatFromBlack { get; set; }
        
        public ChessSquare EnPassantPieceSquare { get; set; }
        
        public ChessSquare SquareWithRookToCastle { get; set; }
    }
}