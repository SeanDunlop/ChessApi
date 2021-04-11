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

        public GameController()
        {
            client = new DatabaseClient();
            reader = new DatabaseReader();
            writer = new DatabaseWriter();
            updater = new DatabaseUpdater();
        }

        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return reader.getOpenGames();
        }
        [Route("/")]
        [HttpGet("{id}")]
        public Game GetGameById([FromRoute] int id)
        {
            return reader.getGame(id);
            //return new Game {chat="Example Chat" };
        }

        [HttpPost]
        public ActionResult<Game> PostGame([FromBody] Game game) 
        {
            int newId = writer.addGame(game);
            return CreatedAtAction(nameof(GetGameById), new {id= newId }, game);
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
