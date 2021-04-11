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

        #region Queries
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
        #endregion
    }
}