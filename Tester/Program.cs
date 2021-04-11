using System;
using ChessApiTwo.Models;
using ChessApiTwo.Database;
using ChessApiTwo.Controllers;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            DatabaseReader reader = new DatabaseReader();
            DatabaseWriter w = new DatabaseWriter();
            DatabaseUpdater up = new DatabaseUpdater();

            User u = reader.getUser(1);
            User u2 = reader.getUser(2);

            int gameid = w.addGame(u, u2);

            //up.updateGame(gameid, "poggers");

            //var open = reader.getOpenGames();
            GameController cont = new GameController();


            var ah = cont.GetGames();
            Console.WriteLine("stop here thanks");
        }
    }
}
