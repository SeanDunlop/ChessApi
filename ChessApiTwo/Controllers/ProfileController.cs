using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessApiTwo.Database;
using ChessApiTwo.Models;

namespace ChessApiTwo.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        DatabaseClient client;
        DatabaseWriter writer;
        DatabaseUpdater updater;
        DatabaseReader reader;

        public ProfileController()
        {
            client = new DatabaseClient();
            reader = new DatabaseReader();
            writer = new DatabaseWriter();
            updater = new DatabaseUpdater();
        }

        [HttpGet]
        public int Login(string username, string pass)
        {
            return reader.login(username, pass);
        }

        [Route("/getProfile")]
        [HttpGet]
        public ActionResult<User> getProfile(int id)
        {
            return reader.getUser(id);
        }

        [Route("/updateProfile")]
        [HttpGet]
        public void setProfile(User u)
        {
            updater.updateUser(u);
        }

        
    }
}
