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
using Microsoft.Extensions.Logging;

namespace Services
{
    public class DbConnection : IDbConnection
    {
        private ILogger<IDbConnection> _logger;
        public DbConnection(ILogger<IDbConnection> logger)
        {
            _logger = logger;
        }
        public void AddBlogArticle(BlogArticleJson blogArticle)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO dbo.blogarticle(BlogId, Title) VALUES(@BlogId, @Title)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sqlQuery, new
                {
                    BlogId = Guid.NewGuid(),
                    Title = blogArticle.Title
                });
            }
        }

        public void AddSections(List<SectionJson> sections, Guid currentBlogId)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO dbo.section(BlogId, SectionId, Header, SubHeader) VALUES(@BlogId, @SectionId, @Header, @SubHeader)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        BlogId = currentBlogId,
                        SectionId = Guid.NewGuid(),
                        Header = sections[i].Header,
                        SubHeader = sections[i].SubHeader
                    });
                }
            }
        }

        public void AddParagraphs(List<Paragraph> paragraphs, Guid sectionId)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO dbo.paragraph(ParagraphId, ParagraphTextArea, SectionId) VALUES(@ParagraphId, @ParagraphTextArea, @SectionId)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                for (int i = 0; i < paragraphs.Count; i++)
                {
                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        ParagraphId = Guid.NewGuid(),
                        ParagraphTextArea = paragraphs[i].ParagraphTextArea,
                        SectionId = sectionId,
                    });
                }
            }
        }

        public void AddImages(List<Image> images)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void DeleteBlogArticle(Guid blogArticleId, Guid sectionId)
        {
            var parameter = new DynamicParameters();
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            string blogArticleQuery = "DELETE FROM dbo.blogarticle WHERE BlogArticleId = @BlogArticleId";
            string sectionQuery = "DELETE FROM dbo.section WHERE BlogArticleId = @BlogArticleId";
            string paragraphQuery = "DELETE FROM dbo.paragraph WHERE SectionId = @SectionId";

            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public List<BlogArticle> GetAllBlogArticles<T>(Func<List<T>, bool> query = null)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.blogArticle";

            var blogArticleItems = new List<BlogArticle>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<BlogArticle>)connection.Query<BlogArticle>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<Section> GetAllSections<T>(Func<List<T>, bool> query = null)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.section";

            var blogArticleItems = new List<Section>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<Section>)connection.Query<Section>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<Paragraph> GetAllParagraphs<T>(Func<List<T>, bool> query = null)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.paragraph";

            var blogArticleItems = new List<Paragraph>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<Paragraph>)connection.Query<Paragraph>(sqlQuery);
            }
            return blogArticleItems;
        }

        public void UpdateItem(Guid blogArticleId, BlogArticle blogArticle)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }
    }
}
