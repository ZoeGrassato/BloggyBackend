using Generics;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Npgsql;
using Models.Mapping;
using System.Linq.Expressions;

namespace Services
{
    public class DbConnection : IDbConnection
    {
        public void AddBlogArticle(BlogArticleJson blogArticle)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void AddSections(List<SectionJson> sections)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void AddParagraphs(List<Paragraph> paragraphs)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void AddImages(List<Image> images)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void DeleteBlogArticle(Guid BlogArticleId)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public List<T> GetAll<T>(Expression<Func<T, bool>> query = null)
        {
            string connectionString = "User ID = root;Password = unearth_Anubis5;Host = localhost; Port = 5432;Database = BloggyData;Pooling = true;Min Pool Size = 0;Max Pool Size = 100;Connection Lifetime = 0;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }

            return null;
        }
    }
}
