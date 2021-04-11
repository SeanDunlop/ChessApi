using System;
using ChessApi.Models;
using ChessApi.Database;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            DatabaseReader reader = new DatabaseReader();
            User u = reader.getUser(1);
            User u2 = reader.getUser(2);

            DatabaseWriter w = new DatabaseWriter();
            //int gameid = w.addGame(u, u2);

            DatabaseUpdater up = new DatabaseUpdater();
            //up.updateGame(gameid, "poggers");

            var open = reader.getOpenGames();
            Console.WriteLine(open.Count);
        }
    }
}
