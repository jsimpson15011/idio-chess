using System;

namespace idiot_chess.Models
{
    public class GameState
    {
        public GameState()
        {
            State = null;
            Player = null;
        }
        
        public string State { get; set; }
        public Player Player { get; set; }
    }
}