using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cassandra;
using DripDropApi.Models;

namespace DripDropApi.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            User user = new User();
            user = CassandraDataProvider.GetUserByID(id);
            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] User newUser)
        {
            CassandraDataProvider.AddUser(newUser.username , newUser.nickname , newUser.password);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            CassandraDataProvider.DeleteUserByUID(id);
            return Ok();
        }
    }
}
