using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessApiTwo.Models;
namespace ChessApiTwo.Database
{
    public class DatabaseClient
    {

        public Game getGame(int id) 
        {
            return new Game { playerB = 1, playerW = 2 };
        }

        public User getUser(int id) 
        {
            return new User { userId = 1, username = "Dawson", elo = 1500 };
        }
    }
}