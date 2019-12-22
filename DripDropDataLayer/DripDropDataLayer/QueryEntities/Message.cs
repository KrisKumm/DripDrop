﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DripDropDataLayer.QueryEntities
{
    public class Message
    {
        public string messageUID { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public DateTime timeSent { get; set; }
        public DateTime timeRead { get; set; }
        public string text { get; set; }
        public string picture { get; set; }
        public string chatUID { get; set; }
        public Message()
        {

        }
    }
}
