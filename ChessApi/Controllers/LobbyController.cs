using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ChessApi.Database;
using ChessApi.Models;

namespace ChessApi.Controllers
{
    public class LobbyController : ApiController
    {
        DatabaseClient client;
        DatabaseWriter writer;
        DatabaseUpdater updater;
        DatabaseReader reader;
        public LobbyController()
        {
            client = new DatabaseClient();
            reader = new DatabaseReader();
            writer = new DatabaseWriter();
            updater = new DatabaseUpdater();
        }

        public List<Game> Get()
        {
            //return "value";
            return reader.getGames();
        }

    }
}