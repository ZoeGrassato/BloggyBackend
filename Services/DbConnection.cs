using Generics;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Npgsql;

namespace Services
{
    public class DbConnection : IDbConnection
    {
        public List<BlogArticle> GetAllBlogs()
        {
            string connectionString = "User ID = root; Password = unearth_Anubis5; Host = localhost; Port = 5432; Database = BloggyData; Pooling = true; Min Pool Size = 0; Max Pool Size = 100; Connection Lifetime = 0;";
            using (var connection = new SqlConnection(connectionString))
            {
                Console.Write("Hello");
            }

            return null;
        }
    }
}
