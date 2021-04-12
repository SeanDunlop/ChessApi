using System;
using ChessApiTwo.Models;
using ChessApiTwo.Database;
using ChessApiTwo.Controllers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

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
            HttpClient client = new HttpClient();

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

            //cont.sendChat(4, "checkmate bitch you suck at chess");
            var help = cont.getUpdate(4);

            //var response = client.GetStringAsync("http://e05a8388588c.ngrok.io/FEN?FEN=rnbqk1nr%2Fpp1p1ppp%2F2p5%2F2b1p3%2F2B1P3%2F5Q2%2FPPPP1PPP%2FRNB1K1NR%20w%20KQkq%20-%200%204");
            //Thread.Sleep(2000);

            cont.botGame(1);

            Console.WriteLine("stop here thanks");
        }
    }
}
