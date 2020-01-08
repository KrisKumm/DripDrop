﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cassandra;
using DripDropApi.Models;

namespace DripDropApi.Controllers
{
    public class MessageController : ApiController
    {
        public IHttpActionResult Get(string id)
        {
            Message message= new Message();
            message = CassandraDataProvider.GetMessageByID(id);
            return Ok(message);
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody] Message message)
        {
            CassandraDataProvider.AddMessage(message.fromWho , message.text , message.picture , message.chatUID);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            CassandraDataProvider.DeleteMessageByUID(id);
            return Ok();
        }
    }
}
