using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ChessApiTwo.Models;

namespace ChessApiTwo.Database
{
    public class DatabaseReader
    {
        private SqlCommand cmd;
        private SqlConnection conn;
        public DatabaseReader()
        {
            conn = new SqlConnection("Server=tcp:chess-server.database.windows.net,1433;Initial Catalog=chess-db;Persist Security Info=False;User ID=chessman;Password=ch3ss1sn3@t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public List<User> getUsers()
        {
            var users = new List<User>();
            cmd = new SqlCommand(_getAllUsers, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User u = new User();
                    u.userId = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                    u.username = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    u.hashpass = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    u.elo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    users.Add(u);
                }
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        public int login(string name, string pass)
        {
            cmd = new SqlCommand(_login, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@pass", pass);
            try
            {
                conn.Open();
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                }
            }
            finally
            {
                conn.Close();
            }
            return -1;
        }

        public User getUser(int id)
        {
            User u = new User();
            cmd = new SqlCommand(_getUser, conn);
            cmd.Parameters.AddWithValue("id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    u.userId = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                    u.username = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    u.hashpass = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    u.elo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                }
            }
            finally
            {
                conn.Close();
            }
            return u;
        }

        public List<Game> getGames()
        {
            var games = new List<Game>();
            cmd = new SqlCommand(_getAllGames, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Game g = new Game();
                    g.gameId = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                    g.playerW = !reader.IsDBNull(1) ? reader.GetInt32(1) : -1;
                    g.playerB = !reader.IsDBNull(2) ? reader.GetInt32(2) : -1;
                    g.pgn = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    g.fen = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    g.chat = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                    games.Add(g);
                }
            }
            finally
            {
                conn.Close();
            }
            return games;
        }

        public Game getGame(int id)
        {
            Game g = new Game();
            cmd = new SqlCommand(_getGame, conn);
            cmd.Parameters.AddWithValue("id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    g.gameId = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                    g.playerW = !reader.IsDBNull(1) ? reader.GetInt32(1) : -1;
                    g.playerB = !reader.IsDBNull(2) ? reader.GetInt32(2) : -1;
                    g.pgn = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    g.fen = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    g.chat = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                }
            }
            finally
            {
                conn.Close();
            }
            return g;
        }

        public List<Game> getOpenGames()
        {
            var games = new List<Game>();
            cmd = new SqlCommand(_getOpenGames, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Game g = new Game();
                    g.gameId = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1;
                    g.playerW = !reader.IsDBNull(1) ? reader.GetInt32(1) : -1;
                    g.playerB = !reader.IsDBNull(2) ? reader.GetInt32(2) : -1;
                    g.pgn = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    g.fen = !reader.IsDBNull(4) ? reader.GetString(4) : null;
                    g.chat = !reader.IsDBNull(5) ? reader.GetString(5) : null;
                    if(reader.GetBoolean(6) == true)
                    {
                        games.Add(g);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return games;
        }

        #region Queries
        string _login = @"
            SELECT
                *
            FROM
                Users
            WHERE
                Username = @name
            AND
                Hashpass = @pass";

        string _getAllUsers = @"
            SELECT
                *
            FROM
                Users";

        string _getUser = @"
            SELECT
                *
            FROM
                Users
            WHERE
                UserID = @id";

        string _getAllGames = @"
            SELECT
                *
            FROM
                Games";

        string _getGame = @"
            SELECT
                *
            FROM
                Games
            WHERE
                GameID = @id";

        string _getOpenGames = @"
            SELECT
                *
            FROM
                Games
            WHERE
                black is null 
            OR
                white is null";
        #endregion
    }
}