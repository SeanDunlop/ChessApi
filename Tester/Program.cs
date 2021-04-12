using System;
using ChessApiTwo.Models;
using ChessApiTwo.Database;
using ChessApiTwo.Controllers;
using System.Collections.Generic;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            DatabaseReader reader = new DatabaseReader();
            DatabaseWriter w = new DatabaseWriter();
            DatabaseUpdater up = new DatabaseUpdater();
            GameController cont = new GameController();

            User u = reader.getUser(1);
            User u2 = reader.getUser(2);

            //int gameid = w.addGame(u, u2);

            //up.updateGame(gameid, "poggers");

            //var open = reader.getOpenGames();

            //int hey = w.newGameBlack(2);

            //cont.quitGame(hey);

            //cont.JoinGame(hey, 1);
            var ahhh = cont.getUpdate(4);
            //cont.UpdateGame(4, "2. Ke2");
            var ahh = cont.getUpdate(4);

            cont.sendChat(4, "checkmate bitch you suck at chess");
            var help = cont.getUpdate(4);

            Console.WriteLine("stop here thanks");
        }
    }
}
