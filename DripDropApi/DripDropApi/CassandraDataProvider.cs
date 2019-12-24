using Cassandra;
using DripDropApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DripDropApi
{
    public class CassandraDataProvider
    {
        #region User

        public static List<User> GetUsers()
        {
            ISession session = SessionManager.GetSession();
            List<User> users = new List<User>();

            if (session == null)
                return null;

            var usersData = session.Execute("select * from User");

            foreach (var userData in usersData)
            {
                User user = new User();
                user.userUID = userData["userUID"] != null ? userData["userUID"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                user.avatar = userData["avatar"] != null ? userData["avatar"].ToString() : string.Empty;
                if (userData["serverUIDsList"] != null)
                    foreach (var item in userData.GetColumn("serverUIDsList").ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.serverUIDsList.Add(string.Empty);
                if (userData["friendUIDsList"] != null)
                    foreach (var item in userData["friendUIDsList"].ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.friendUIDsList.Add(string.Empty);
                users.Add(user);
            }

            session.Cluster.Shutdown();

            return users;
        }

        public static string GetUserUIDByUsername(string username)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where username='" + username + "'").FirstOrDefault();

            UID = userData["userUID"] != null ? userData["userUID"].ToString() : string.Empty;

            session.Cluster.Shutdown();

            return UID;
        }

        public static User GetUserByID(string userUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where userUID='" + userUID + "'").FirstOrDefault();

            if (userData != null)
            {
                user.userUID = userData["userUID"] != null ? userData["userUID"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                user.avatar = userData["avatar"] != null ? userData["avatar"].ToString() : string.Empty;
                if (userData["serverUIDsList"] != null)
                    foreach (var item in userData.GetColumn("serverUIDsList").ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.serverUIDsList.Add(string.Empty);
                if (userData["friendUIDsList"] != null)
                    foreach (var item in userData["friendUIDsList"].ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.friendUIDsList.Add(string.Empty);
            }

            session.Cluster.Shutdown();

            return user;
        }

        public static User GetUserByUsernameAndPassword(string username, string password)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return null;

            Row userData = session.Execute("select * from User where username='" + username + "' and password='" + password + "'").FirstOrDefault();

            if (userData != null)
            {
                user.userUID = userData["userUID"] != null ? userData["userUID"].ToString() : string.Empty;
                user.username = userData["username"] != null ? userData["username"].ToString() : string.Empty;
                user.nickname = userData["nickname"] != null ? userData["nickname"].ToString() : string.Empty;
                user.password = userData["password"] != null ? userData["password"].ToString() : string.Empty;
                user.avatar = userData["avatar"] != null ? userData["avatar"].ToString() : string.Empty;
                if (userData["serverUIDsList"] != null)
                    foreach (var item in userData.GetColumn("serverUIDsList").ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.serverUIDsList.Add(string.Empty);
                if (userData["friendUIDsList"] != null)
                    foreach (var item in userData["friendUIDsList"].ToString())
                    {
                        user.serverUIDsList.Add(item.ToString());
                    }
                else
                    user.friendUIDsList.Add(string.Empty);
            }

            session.Cluster.Shutdown();

            return user;
        }

        public static void AddUser(string username, string nickname, string password)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("insert into User (userUID, username, nickname, password, avatar, serverUIDsList, friendUIDsList) values (uuid(), '" + username + "', '" + nickname + "', '" + password + "', '', [], [])");

            session.Cluster.Shutdown();
        }

        public static void AddUserAvatar(string userUID, string avatar)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("update User set avatar='" + avatar + "' where userUID='" + userUID + "'");

            session.Cluster.Shutdown();
        }

        public static void AddUserServerUID(string userUID, string serverUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return;

            RowSet userData = session.Execute("update User set serverUIDsList = serverUIDsList + ['" + serverUID + "'] where userUID='" + userUID + "'");

            session.Cluster.Shutdown();
        }

        public static void AddUserFriendUID(string userUID, string friendUID)
        {
            ISession session = SessionManager.GetSession();
            User user = new User();

            if (session == null)
                return;

            RowSet userData = session.Execute("update User set friendUIDsList = friendUIDsList + ['" + friendUID + "'] where userUID='" + userUID + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteUserByUsername(string username)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("delete from User where username='" + username + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteUserByUID(string userUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet userData = session.Execute("delete from User where userUID='" + userUID + "'");

            session.Cluster.Shutdown();
        }

        #endregion

        #region Server

        public static List<Server> GetServers()
        {
            ISession session = SessionManager.GetSession();
            List<Server> servers = new List<Server>();

            if (session == null)
                return null;

            var serversData = session.Execute("select * from Server");

            foreach (var serverData in serversData)
            {
                Server server = new Server();
                server.serverUID = serverData["serverUID"] != null ? serverData["serverUID"].ToString() : string.Empty;
                server.name = serverData["name"] != null ? serverData["name"].ToString() : string.Empty;
                server.password = serverData["password"] != null ? serverData["password"].ToString() : string.Empty;
                if (serverData["userUIDsList"] != null)
                    foreach (var item in serverData.GetColumn("userUIDsList").ToString())
                    {
                        server.userUIDsList.Add(item.ToString());
                    }
                else
                    server.userUIDsList.Add(string.Empty);
                if (serverData["adminUIDsList"] != null)
                    foreach (var item in serverData["adminUIDsList"].ToString())
                    {
                        server.adminUIDsList.Add(item.ToString());
                    }
                else
                    server.adminUIDsList.Add(string.Empty);
                server.privateS = serverData["privateS"] != null ? (Boolean)serverData["privateS"] : false;
                server.creationTime = serverData["creationTime"] != null ? (DateTime)serverData["creationTime"] : DateTime.Now;
                servers.Add(server);
            }

            session.Cluster.Shutdown();

            return servers;
        }

        public static string GetServerUIDByName(string name)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row serverData = session.Execute("select * from Server where username='" + name + "'").FirstOrDefault();

            UID = serverData["serverUID"] != null ? serverData["serverUID"].ToString() : string.Empty;

            session.Cluster.Shutdown();

            return UID;
        }

        public static Server GetServerByID(string serverUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return null;

            Row serverData = session.Execute("select * from User where userUID='" + serverUID + "'").FirstOrDefault();

            if (serverData != null)
            {
                server.serverUID = serverData["serverUID"] != null ? serverData["serverUID"].ToString() : string.Empty;
                server.name = serverData["name"] != null ? serverData["name"].ToString() : string.Empty;
                server.password = serverData["password"] != null ? serverData["password"].ToString() : string.Empty;
                if (serverData["userUIDsList"] != null)
                    foreach (var item in serverData.GetColumn("userUIDsList").ToString())
                    {
                        server.userUIDsList.Add(item.ToString());
                    }
                else
                    server.userUIDsList.Add(string.Empty);
                if (serverData["adminUIDsList"] != null)
                    foreach (var item in serverData["adminUIDsList"].ToString())
                    {
                        server.adminUIDsList.Add(item.ToString());
                    }
                else
                    server.adminUIDsList.Add(string.Empty);
                server.privateS = serverData["privateS"] != null ? (Boolean)serverData["privateS"] : false;
                server.creationTime = serverData["creationTime"] != null ? (DateTime)serverData["creationTime"] : DateTime.Now;
            }

            session.Cluster.Shutdown();

            return server;
        }

        public static void AddServer(string name, string password, bool privateS)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet serverData = session.Execute("insert into Server (serverUID, name, password, userUIDsList, adminUIDsList, privateS, creationTime) values (uuid(), '" + name + "', '" + password + "', [], [], '" + privateS + "', now())");

            session.Cluster.Shutdown();
        }

        public static void AddServerUserUID(string serverUID, string userUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set userUIDsList = userUIDsList + ['" + userUID + "'] where serverUID='" + serverUID + "'");

            session.Cluster.Shutdown();
        }

        public static void AddServerAdminUID(string serverUID, string adminUID)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set adminUIDsList = adminUIDsList + ['" + adminUID + "'] where serverUID='" + serverUID + "'");

            session.Cluster.Shutdown();
        }

        public static void AddServerPrivate(string serverUID, bool privateS)
        {
            ISession session = SessionManager.GetSession();
            Server server = new Server();

            if (session == null)
                return;

            RowSet serverData = session.Execute("update Server set privateS='" + privateS + "' where serverUID='" + serverUID + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteServerByName(string name)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet serverData = session.Execute("delete from Server where name='" + name + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteServerByUID(string serverUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet serverData = session.Execute("delete from Server where serverUID='" + serverUID + "'");

            session.Cluster.Shutdown();
        }

        #endregion

        #region Chat

        public static List<Chat> GetChats()
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData = session.Execute("select * from Chat");

            foreach (var chatData in chatsData)
            {
                Chat chat = new Chat();
                chat.chatUID = chatData["chatUID"] != null ? chatData["chatUID"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["userUIDsList"] != null)
                    foreach (var item in chatData.GetColumn("userUIDsList").ToString())
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
                chat.creationTime = chatData["creationTime"] != null ? (DateTime)chatData["creationTime"] : DateTime.Now;
                chat.serverUID = chatData["serverUID"] != null ? chatData["serverUID"].ToString() : string.Empty;
                chats.Add(chat);
            }

            session.Cluster.Shutdown();

            return chats;
        }

        public static List<Chat> GetChatsByServerUID(string serverUID)
        {
            ISession session = SessionManager.GetSession();
            List<Chat> chats = new List<Chat>();

            if (session == null)
                return null;

            var chatsData = session.Execute("select * from Chat where serverUID='" + serverUID + "'");

            foreach (var chatData in chatsData)
            {
                Chat chat = new Chat();
                chat.chatUID = chatData["chatUID"] != null ? chatData["chatUID"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["userUIDsList"] != null)
                    foreach (var item in chatData.GetColumn("userUIDsList").ToString())
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
                chat.creationTime = chatData["creationTime"] != null ? (DateTime)chatData["creationTime"] : DateTime.Now;
                chat.serverUID = chatData["serverUID"] != null ? chatData["serverUID"].ToString() : string.Empty;
                chats.Add(chat);
            }

            session.Cluster.Shutdown();

            return chats;
        }

        public static string GetChatUIDByName(string name)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row chatData = session.Execute("select * from Chat where name='" + name + "'").FirstOrDefault();

            UID = chatData["chatUID"] != null ? chatData["chatUID"].ToString() : string.Empty;

            session.Cluster.Shutdown();

            return UID;
        }

        public static Chat GetChatByID(string chatUID)
        {
            ISession session = SessionManager.GetSession();
            Chat chat = new Chat();

            if (session == null)
                return null;

            Row chatData = session.Execute("select * from Chat where chatUID='" + chatUID + "'").FirstOrDefault();

            if (chatData != null)
            {
                chat.chatUID = chatData["chatUID"] != null ? chatData["chatUID"].ToString() : string.Empty;
                chat.name = chatData["name"] != null ? chatData["name"].ToString() : string.Empty;
                if (chatData["userUIDsList"] != null)
                    foreach (var item in chatData.GetColumn("userUIDsList").ToString())
                    {
                        chat.userUIDsList.Add(item.ToString());
                    }
                else
                    chat.userUIDsList.Add(string.Empty);
                chat.creationTime = chatData["creationTime"] != null ? (DateTime)chatData["creationTime"] : DateTime.Now;
                chat.serverUID = chatData["serverUID"] != null ? chatData["serverUID"].ToString() : string.Empty;
            }

            session.Cluster.Shutdown();

            return chat;
        }

        public static void AddChat(string name, string serverUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("insert into Chat (chatUID, name, userUIDsList, creationTime, serverUID) values (uuid(), '" + name + "', [], now(), '" + serverUID + "')");

            session.Cluster.Shutdown();
        }

        public static void AddChatUserUID(string chatUID, string userUID)
        {
            ISession session = SessionManager.GetSession();
            Chat chat = new Chat();

            if (session == null)
                return;

            RowSet chatData = session.Execute("update Chat set userUIDsList = userUIDsList + ['" + userUID + "'] where chatUID='" + chatUID + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteChatByName(string name)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Chat where name='" + name + "'");

            session.Cluster.Shutdown();
        }

        public static void DeleteChatByUID(string chatUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Chat where chatUID='" + chatUID + "'");

            session.Cluster.Shutdown();
        }

        #endregion

        #region Message

        public static List<Message> GetMessages()
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.messageUID = messageData["messageUID"] != null ? messageData["messageUID"].ToString() : string.Empty;
                message.fromWho = messageData["fromWho"] != null ? messageData["fromWho"].ToString() : string.Empty;
                message.timeSent = messageData["timeSent"] != null ? (DateTime)messageData["timeSent"] : DateTime.Now;
                message.timeRead = messageData["timeRead"] != null ? (DateTime)messageData["timeRead"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatUID"] != null ? messageData["chatUID"].ToString() : string.Empty;
                messages.Add(message);
            }

            session.Cluster.Shutdown();

            return messages;
        }

        public static List<Message> GetMessagesByChatUID(string chatUID)
        {
            ISession session = SessionManager.GetSession();
            List<Message> messages = new List<Message>();

            if (session == null)
                return null;

            var messagesData = session.Execute("select * from Message where chatUID='" + chatUID + "'");

            foreach (var messageData in messagesData)
            {
                Message message = new Message();
                message.messageUID = messageData["messageUID"] != null ? messageData["messageUID"].ToString() : string.Empty;
                message.fromWho = messageData["fromWho"] != null ? messageData["fromWho"].ToString() : string.Empty;
                message.timeSent = messageData["timeSent"] != null ? (DateTime)messageData["timeSent"] : DateTime.Now;
                message.timeRead = messageData["timeRead"] != null ? (DateTime)messageData["timeRead"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatUID"] != null ? messageData["chatUID"].ToString() : string.Empty;
                messages.Add(message);
            }

            session.Cluster.Shutdown();

            return messages;
        }

        public static string GetMessageUIDByName(string fromWho, string chatUID)
        {
            ISession session = SessionManager.GetSession();
            String UID = "";

            if (session == null)
                return null;

            Row messagesData = session.Execute("select * from Message where fromWho='" + fromWho + "' and chatUID='" + chatUID + "'").FirstOrDefault();

            UID = messagesData["messageUID"] != null ? messagesData["messageUID"].ToString() : string.Empty;

            session.Cluster.Shutdown();

            return UID;
        }

        public static Message GetMessageByID(string messageUID)
        {
            ISession session = SessionManager.GetSession();
            Message message = new Message();

            if (session == null)
                return null;

            Row messageData = session.Execute("select * from Message where messageUID='" + messageUID + "'").FirstOrDefault();

            if (messageData != null)
            {
                message.messageUID = messageData["messageUID"] != null ? messageData["messageUID"].ToString() : string.Empty;
                message.fromWho = messageData["fromWho"] != null ? messageData["fromWho"].ToString() : string.Empty;
                message.timeSent = messageData["timeSent"] != null ? (DateTime)messageData["timeSent"] : DateTime.Now;
                message.timeRead = messageData["timeRead"] != null ? (DateTime)messageData["timeRead"] : DateTime.Now;
                message.text = messageData["text"] != null ? messageData["text"].ToString() : string.Empty;
                message.picture = messageData["picture"] != null ? messageData["picture"].ToString() : string.Empty;
                message.chatUID = messageData["chatUID"] != null ? messageData["chatUID"].ToString() : string.Empty;
            }

            session.Cluster.Shutdown();

            return message;
        }

        public static void AddMessage(string fromWho, string text, string picture, string chatUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("insert into Message (messageUID, fromWho, timeSent, timeRead, text, picture, chatUID) values (uuid(), '" + fromWho + "', now(), now(), '" + text + "', '" + picture + "', '" + chatUID + "')");

            session.Cluster.Shutdown();
        }

        public static void DeleteMessageByUID(string messageUID)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return;

            RowSet chatData = session.Execute("delete from Message where messageUID='" + messageUID + "'");

            session.Cluster.Shutdown();
        }

        #endregion
    }
}