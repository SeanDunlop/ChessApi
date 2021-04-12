using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessApiTwo.Database;
using ChessApiTwo.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace ChessApiTwo.Controllers
{
    [ApiController]
    [Route("api/Games")]
    public class GameController : ControllerBase
    {
        DatabaseClient client;
        DatabaseWriter writer;
        DatabaseUpdater updater;
        DatabaseReader reader;
        Random rand;
        HttpClient httpclient = new HttpClient();

        public GameController()
        {
            client = new DatabaseClient();
            reader = new DatabaseReader();
            writer = new DatabaseWriter();
            updater = new DatabaseUpdater();
            rand = new Random();
        }

        [HttpGet]
        public IEnumerable<LobbyGame> GetGames()
        {
            List<Game> games = reader.getOpenGames();
            List<LobbyGame> lgs = new List<LobbyGame>();
            foreach(Game g in games)
            {
                LobbyGame lg = new LobbyGame();
                User u;
                if(g.playerW == -1)
                {
                    u = reader.getUser(g.playerB);
                    lg.gameId = g.gameId;
                    lg.hostName = u.username;
                    lg.hostElo = u.elo;
                    lg.hostColor = "Black";
                }
                else
                {
                    u = reader.getUser(g.playerW);
                    lg.gameId = g.gameId;
                    lg.hostName = u.username;
                    lg.hostElo = u.elo;
                    lg.hostColor = "White";
                }
                lgs.Add(lg);
            }
            return lgs;
        }
        [Route("/")]
        [HttpGet("{id}")]
        public Game GetGameById([FromRoute] int id)
        {
            return reader.getGame(id);
            //return new Game {chat="Example Chat" };
        }

        [HttpGet]
        [Route("/new")]
        public ActionResult<Game> NewLobby(int id) 
        {
            int newId;
            Game g;
            if(rand.NextDouble() > 0.5)
            {
                newId = writer.newGameWhite(id);
                g = reader.getGame(newId);
            }
            else
            {
                newId = writer.newGameBlack(id);
                g = reader.getGame(newId);
            }
            return g;
        }

        [Route("/quit")]
        [HttpGet]
        public void quitGame(int gameId)
        {
            updater.stopGame(gameId);
        }

        [Route("/join")]
        [HttpGet]
        public ActionResult<Game> JoinGame(int gameId, int userId)
        {
            Game g = reader.getGame(gameId);
            if (g.playerW == -1)
            {
                g.playerW = userId;
                updater.joinGameWhite(g);
            }
            else
            {
                g.playerB = userId;
                updater.joinGameBlack(g);
            }
            return g;
        }

        [Route("/chatSend")]
        [HttpGet]
        public void sendChat(int id, string chat)
        {
            var game = reader.getGame(id);
            game.chat = chat;
            updater.updateGame(game);
        }

        [Route("/move")]
        [HttpGet]
        public void UpdateGame(int id, string fen) 
        {
            Game g = reader.getGame(id);
            string format = fen.Replace("/", "%2F");
            format = format.Replace(" ", "%20");
            
            if (g.playerB == 5 || g.playerW == 5)
            {
                var response = httpclient.GetStringAsync("http://e05a8388588c.ngrok.io/FEN?FEN=" + format);
                Thread.Sleep(1000);
                string newFen = response.Result;
                updater.updateGame(id, newFen);
            }
            else
            {
                updater.updateGame(id, fen);
            }
            
        }

        [Route("/bot")]
        [HttpGet]
        public ActionResult<Game> botGame(int id)
        {
            ActionResult<Game> g = NewLobby(id);
            return JoinGame(g.Value.gameId, 5);
        }

        [Route("/update")]
        [HttpGet]
        public ActionResult<Game> getUpdate(int id)
        {
            return reader.getGame(id);
        }
    }
}
