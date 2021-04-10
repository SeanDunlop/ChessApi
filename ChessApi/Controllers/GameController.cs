﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChessApi.Database;
using ChessApi.Models;

namespace ChessApi.Controllers
{
    public class GameController : ApiController
    {
        // GET: Game

        DatabaseClient client;
        public GameController()
        {
            client = new DatabaseClient();
        }

        // GET api/values/5
        public Game Get(int id)
        {
            //return "value";
            return client.getGame(id);
        }

        // POST api/values
        public void Post([FromBody] Game newGame)
        {
            //client makes new user
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] User updatedGame)
        {
            //client updates a user object
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            //delete user with id from database
        }
    }
}
}