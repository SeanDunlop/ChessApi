using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessApiTwo.Models
{
    public class Game
    {
        public int gameId { get; set; }
        public int playerW { get; set; }
        public int playerB { get; set; }

        public String pgn { get; set; }
        public String fen {get;set;}
        public String chat { get; set; }

    }
}