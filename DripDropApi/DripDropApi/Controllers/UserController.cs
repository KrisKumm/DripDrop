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
        [Route("GetUsers")]
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users = CassandraDataProvider.GetUsers();
            return users;
        }
    }
}
