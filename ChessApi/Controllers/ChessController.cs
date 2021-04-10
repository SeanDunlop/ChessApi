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
    public class ChessController : ApiController
    {
        DatabaseClient client;
        public ChessController() 
        {
            client = new DatabaseClient();
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public User Get(int id)
        {
            //return "value";
            return client.getUser(id);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}