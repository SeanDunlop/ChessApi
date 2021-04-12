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
            if(rand.Next(0,1) == 1)
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

        [Route("/")]
        [HttpPut("{id}")]
        public ActionResult<Game> UpdateGame([FromBody] Game game) 
        {
            //UPDATE THE GAME
            return NoContent();
        }
    }
}
