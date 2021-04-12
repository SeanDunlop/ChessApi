using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ChessApiTwo.Models;
namespace ChessApiTwo.Database
{
    public class DatabaseUpdater
    {
        private SqlCommand cmd;
        private SqlConnection conn;
        public DatabaseUpdater()
        {
            conn = new SqlConnection("Server=tcp:chess-server.database.windows.net,1433;Initial Catalog=chess-db;Persist Security Info=False;User ID=chessman;Password=ch3ss1sn3@t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public void execute(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public void joinGameBlack(Game g)
        {
            cmd = new SqlCommand(_joinGameBlack, conn);
            cmd.Parameters.AddWithValue("@b", g.playerB);
            cmd.Parameters.AddWithValue("id", g.gameId);
            execute(cmd);
        }

        public void joinGameWhite(Game g)
        {
            cmd = new SqlCommand(_joinGameWhite, conn);
            cmd.Parameters.AddWithValue("@w", g.playerW);
            cmd.Parameters.AddWithValue("id", g.gameId);
            execute(cmd);
        }

        public void updateGame(Game g)
        {
            cmd = new SqlCommand(_updateGameWChat, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@fen", g.fen);
                cmd.Parameters.AddWithValue("@id", g.gameId);
                cmd.Parameters.AddWithValue("@chat", g.chat);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public void stopGame(int id)
        {
            cmd = new SqlCommand(_stopGame, conn);
            cmd.Parameters.AddWithValue("@id", id);
            execute(cmd);
        }

        public void updateGame(int id, string FEN)
        {
            cmd = new SqlCommand(_updateGame, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@fen", FEN);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateUser(User u)
        {
            cmd = new SqlCommand(_updateUser, conn);
            cmd.Parameters.AddWithValue("@id", u.userId);
            cmd.Parameters.AddWithValue("@name", u.username);
            execute(cmd);
        }

        #region Queries
        string _stopGame = @"
            UPDATE
                Games
            SET
                Active = 0
            WHERE
                GameID = @id";

        string _joinGameBlack = @"
            UPDATE
                Games
            SET
                Black = @b
            WHERE  
                GameID = @id";

        string _joinGameWhite = @"
            UPDATE
                Games
            SET
                White = @w
            WHERE
                GameID = @id";
        
        string _updateGame = @"
            UPDATE
                Games
            SET
                FEN = @fen
            WHERE
                GameID = @id";

        string _updateGameWChat = @"
            UPDATE
                Games
            SET
                FEN = @fen,
                chat = @chat
            WHERE
                GameID = @id";

        string _updateUser = @"
            UPDATE
                Users
            SET
                Username = @name
            WHERE
                UserID = @id";
        #endregion
    }
}