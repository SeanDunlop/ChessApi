using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ChessApiTwo.Models;

namespace ChessApiTwo.Database
{
    public class DatabaseWriter
    {
        private SqlCommand cmd;
        private SqlConnection conn;
        private Random rand;
        public DatabaseWriter()
        {
            conn = new SqlConnection("Server=tcp:chess-server.database.windows.net,1433;Initial Catalog=chess-db;Persist Security Info=False;User ID=chessman;Password=ch3ss1sn3@t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            rand = new Random();
        }

        public int addUser(User u)
        {
            cmd = new SqlCommand(_addUser,conn);
            cmd.Parameters.AddWithValue("@name", u.username);
            cmd.Parameters.AddWithValue("@pass", u.hashpass);
            return executeWithId(cmd);
        }

        public int addGame(Game g)
        {
            cmd = new SqlCommand(_addGame, conn);
            cmd.Parameters.AddWithValue("@w", g.playerW);
            cmd.Parameters.AddWithValue("@b", g.playerB);
            int val =  executeWithId(cmd);
            return val;
        }

        public int addGame(User w, User b)
        {
            cmd = new SqlCommand(_addGame, conn);
            cmd.Parameters.AddWithValue("@w", w.userId);
            cmd.Parameters.AddWithValue("@b", b.userId);
            return executeWithId(cmd);
        }

        public int addGame(int w, int b)
        {
            cmd = new SqlCommand(_addGame, conn);
            cmd.Parameters.AddWithValue("@w", w);
            cmd.Parameters.AddWithValue("@b", b);
            return executeWithId(cmd);
        }

        public int newGameWhite(int w)
        {
            cmd = new SqlCommand(_addNewGameWhite, conn);
            cmd.Parameters.AddWithValue("@w", w);
            return executeWithId(cmd);
        }

        public int newGameBlack(int b)
        {
            cmd = new SqlCommand(_addNewGameBlack, conn);
            cmd.Parameters.AddWithValue("@b", b);
            return executeWithId(cmd);
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

        public int executeWithId(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return (int)cmd.ExecuteScalar();
            }

            finally
            {
                cmd.Connection.Close();
            }
        }

        #region Queries
        string _addUser = @"
            INTSERT INTO
                Users
            OUTPUT
                Inserted.UserID
            VALUES(
                @name,
                @pass,
                1500)";

        string _addGame = @"
            INSERT INTO 
                Games
            OUTPUT
                Inserted.GameID
            VALUES(
                @w,
                @b,
                '',
                'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1',
                '',
                1)";

        string _addNewGameWhite = @"
            INSERT INTO 
                Games
            OUTPUT
                Inserted.GameID
            VALUES(
                @w,
                null,
                '',
                'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1',
                '',
                1)";

        string _addNewGameBlack = @"
            INSERT INTO 
                Games
            OUTPUT
                Inserted.GameID
            VALUES(
                null,
                @b,
                '',
                'rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1',
                '',
                1)";
        #endregion
    }
}