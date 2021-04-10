using System;
using ChessApi.Models;
using ChessApi.Database;

namespace Tester
{
    public class Program
    {
        static void Main(string[] args)
        {
            DatabaseReader reader = new DatabaseReader();
            User u = reader.getUser(1);
            Console.WriteLine(u.username);
        }
    }
}
