using System.Collections.Generic;

namespace idiot_chess.Models
{
    public class ChessPiece
    {
        public ChessPiece()
        {}
        
        public ChessPiece(string color, string name)
        {
            Dictionary<string, int> pieceValues = new Dictionary<string, int>();
            pieceValues.TryAdd("pawn", 1);
            pieceValues.TryAdd("knight", 3);
            pieceValues.TryAdd("bishop", 3);
            pieceValues.TryAdd("rook", 5);
            pieceValues.TryAdd("queen", 9);
            pieceValues.TryAdd("king", 9999);

            Name = name;
            Color = color;
            Value = pieceValues[name];
        }





        public string Color { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        
        public bool HasMoved { get; set; }
        
        
    }
}