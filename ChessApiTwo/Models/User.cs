using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessApiTwo.Models
{
    public class User
    {
        public string username { get; set; }
        public int userId { get; set; }
        public string hashpass { get; set; }

        public int elo { get; set; }

    }
}