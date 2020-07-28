﻿using System;
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

        public void AddBlogArticle(BlogArticleAccessObj blogArticle)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO blogarticle(BlogId, Title) VALUES(@BlogId, @Title)";
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
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO section(BlogId, SectionId, Header, SubHeader) VALUES(@BlogId, @SectionId, CAST(@Header as json), CAST(@SubHeader as json))";
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
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO paragraph(ParagraphId, ParagraphTextArea, SectionId) VALUES(@ParagraphId, @ParagraphTextArea, @SectionId)";
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
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            using (var connection = new NpgsqlConnection(connectionString))
            {

            }
        }

        public void DeleteBlogArticle(Guid blogArticleId, Guid sectionId)
        {
            var parameter = new DynamicParameters();
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            string blogArticleQuery = "DELETE FROM blogarticle WHERE BlogArticleId = @BlogArticleId";
            string sectionQuery = "DELETE FROM dbo.section WHERE BlogArticleId = @BlogArticleId";
            string paragraphQuery = "DELETE FROM dbo.paragraph WHERE SectionId = @SectionId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                
            }
        }

        public List<BlogArticleAccessObj> GetAllBlogArticles()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = $"SELECT * FROM blogArticle";

            var blogArticleItems = new List<BlogArticleAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<BlogArticleAccessObj>)connection.Query<BlogArticleAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<SectionAccessObj> GetAllSections()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM section";

            var blogArticleItems = new List<SectionAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<SectionAccessObj>)connection.Query<SectionAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public List<ParagraphAccessObj> GetAllParagraphs()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM paragraph";

            var blogArticleItems = new List<ParagraphAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                blogArticleItems = (List<ParagraphAccessObj>)connection.Query<ParagraphAccessObj>(sqlQuery);
            }
            return blogArticleItems;
        }

        public void UpdateItem(UpdateBlogArticleAccessObj blogArticle)
        {
            
        }
        public void UpdateTitle(BlogArticleAccessObj blogArticle)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                    string sqlQuery = $"UPDATE blogarticle SET title = @Title WHERE blogarticleid = @BlogArticleId";

                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        Title = blogArticle.Title,
                        BlogArticleId = blogArticle.BlogArticleId
                    });
            }
        }

        public void UpdateSections(List<SectionAccessObj> sections)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            
            using (var connection = new NpgsqlConnection(connectionString))
            {
                for(int i =0; i < sections.Count; i++)
                {
                    string sqlQuery = $"UPDATE section SET header = @Header, subheader = @Subheader WHERE sectionid = @SectionId";

                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        Header = sections[i].Header,
                        Subheader = sections[i].SubHeader,
                        SectionId = sections[i].SectionId
                    });
                }
            }
        }

        public void UpdateParagraphs(List<ParagraphAccessObj> paragraphs)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                for (int i = 0; i < paragraphs.Count; i++)
                {
                    string sqlQuery = $"UPDATE section SET paragraphtextarea = @ParagrahtextArea WHERE paragraphid = @ParagraphId";

                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        ParagrahtextArea = paragraphs[i].ParagraphTextArea,
                        ParagraphId = paragraphs[i].ParagraphId
                    });
                }
            }
        }
        public void UpdateImages(List<ImageAccessObj> images)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                for (int i = 0; i < images.Count; i++)
                {
                    string sqlQuery = $"UPDATE section SET bytecode = @ByteCode WHERE imageid = @ImageId";

                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        ByteCode = images[i].BytesImages,
                        ImageId = images[i].ImageId
                    });
                }
            }
        }

    }
}
