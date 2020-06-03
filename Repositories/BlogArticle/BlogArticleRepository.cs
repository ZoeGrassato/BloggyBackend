using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Npgsql;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Repositories.BlogArticle.Models;
using Repositories.BlogArticle.Models.JsonMappingModels;

namespace Repositories.BlogArticle
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private ILogger<IBlogArticleRepository> _logger;
        public BlogArticleRepository(ILogger<IBlogArticleRepository> logger)
        {
            _logger = logger;
        }

        public BlogArticleAccessObj QueryBlogArticle()
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.blogarticle(BlogId, Title) VALUES(@BlogId, @Title)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var affectedRows = connection.Execute(sqlQuery, new
                {
                    BlogId = Guid.NewGuid(),
                    Title = blogArticle.Title
                });
            }
        }


        public void AddBlogArticle(BlogArticleAccessObj blogArticle)
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

        public void AddSections(List<SectionJsonAccessObj> sections, Guid currentBlogId)
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

        public void AddParagraphs(List<ParagraphAccessObj> paragraphs, Guid sectionId)
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

        public void AddImages(List<ImageAccessObj> images)
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

        public List<BlogArticleAccessObj> GetAllBlogArticles()
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = $"SELECT * FROM dbo.blogArticle";

            var blogArticleItems = new List<BlogArticleAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<BlogArticleAccessObj>)connection.Query<BlogArticleAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<SectionAccessObj> GetAllSections<T>(Func<List<T>, bool> query = null)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.section";

            var blogArticleItems = new List<SectionAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<SectionAccessObj>)connection.Query<SectionAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<ParagraphAccessObj> GetAllParagraphs<T>(Func<List<T>, bool> query = null)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM dbo.paragraph";

            var blogArticleItems = new List<ParagraphAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<ParagraphAccessObj>)connection.Query<ParagraphAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public void UpdateItem(Guid blogArticleId, BlogArticleAccessObj blogArticle)
        {
            string connectionString = "UserID=root;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }
    }
}
