using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessApiTwo.Models
{
    public class LobbyGame
    {
        public int gameId { get; set; }
        public string hostName { get; set; }
        public int hostElo { get; set; }
        public string hostColor { get; set; }
    }
}
