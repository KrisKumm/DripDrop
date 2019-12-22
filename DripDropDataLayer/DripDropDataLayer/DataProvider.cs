using Cassandra;
using DripDropDataLayer.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DripDropDataLayer
{
    public static class DataProvider
    {
        #region User

        public static User GetUser(string userUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return null;

            //Row userData = session.Execute("select * from User where userUID='" + userUID + "'").FirstOrDefault();

            //if(userData != null)
            //{
            //    user.userUID = userData["userUID"] != null ? userData["userUID"].ToString() : string.Empty;
            //    user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
            //    user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
            //    user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
            //    user.avatar = userData["avatar"] != null ? userData["avatar"].ToString() : string.Empty;
            //    user.serverUIDsList = userData["serverUIDsList"] != null ? userData["serverUIDsList"].ToString() : string.Empty;
            //    user.friendUIDsList = userData["friendUIDsList"] != null ? userData["friendUIDsList"].ToString() : string.Empty;
            //}

            return user;
        }

        #endregion

        #region Server

        #endregion

        #region Chat

        #endregion

        #region Message

        #endregion
    }
}
