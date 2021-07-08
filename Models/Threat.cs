using System.Collections.Generic;

namespace idiot_chess.Models
{
    public class Threat
    {
        public Threat()
        {
        }

        public Threat(string key, ChessPiece piece)
        {
            Piece = piece;
            Key = key;
        }


        public string Key { get; set; }
        public ChessPiece Piece { get; set; }
        
    }
}