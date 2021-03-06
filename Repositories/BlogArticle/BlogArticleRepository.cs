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
using Repositories.Exceptions;
using Newtonsoft.Json;
using Repositories.Serialization;

namespace Repositories.BlogArticle
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private ILogger<IBlogArticleRepository> _logger;
        public BlogArticleRepository(ILogger<IBlogArticleRepository> logger)
        {
            _logger = logger;
        }

        public void AddBlogArticle(BlogArticleAccessObj blogArticle, Guid uniqueIdentifier)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO blogarticle(BlogId, Title) VALUES(@BlogId, @Title)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        BlogId = uniqueIdentifier,
                        Title = blogArticle.Title
                    });
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
        }

        public void AddSection(SectionJsonAccessObj section, Guid currentBlogId)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "INSERT INTO section(BlogId, SectionId, Header, SubHeader) VALUES(@BlogId, @SectionId, CAST(@Header as json), CAST(@SubHeader as json))";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        BlogId = currentBlogId,
                        SectionId = section.SectionId,
                        Header = section.Header,
                        SubHeader = section.SubHeader
                    });
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
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
                    try
                    {
                        var affectedRows = connection.Execute(sqlQuery, new
                        {
                            ParagraphId = paragraphs[i].ParagraphId,
                            ParagraphTextArea = paragraphs[i].ParagraphTextArea,
                            SectionId = sectionId,
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
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

        public void DeleteBlogArticle(Guid blogArticleId)
        {
            var parameter = new DynamicParameters();
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            string blogArticleQuery = "DELETE FROM blogarticle WHERE BlogId = @BlogId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    var affectedRows = connection.Execute(blogArticleQuery, new
                    {
                        BlogId = blogArticleId
                    });
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
        }

        public void DeleteSections(List<Guid> sectionIds)
        {
            var parameter = new DynamicParameters();
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            string blogArticleQuery = "DELETE FROM section WHERE SectionId = @SectionId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                foreach(var sectionId in sectionIds)
                {
                    try
                    {
                        var affectedRows = connection.Execute(blogArticleQuery, new
                        {
                            SectionId = sectionId
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
                }
            }
        }

        public void DeleteParagraphs(List<Guid> paragraphIds)
        {
            var parameter = new DynamicParameters();
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            string blogArticleQuery = "DELETE FROM paragraph WHERE ParagraphId = @ParagraphId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                foreach (var paragraphId in paragraphIds)
                {
                    try
                    {
                        var affectedRows = connection.Execute(blogArticleQuery, new
                        {
                            ParagraphId = paragraphId
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
                }
            }
        }

        public List<BlogArticleAccessObj> GetAllBlogArticles()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = $"SELECT * FROM blogArticle";

            var blogArticleItems = new List<BlogArticleAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    blogArticleItems = (List<BlogArticleAccessObj>)connection.Query<BlogArticleAccessObj>(sqlQuery);
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
            return blogArticleItems;
        }

        public List<GetAllSectionsAccessObject> GetAllSections()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM section";

            var sections = new List<GetAllSectionsAccessObject>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    sections = (List<GetAllSectionsAccessObject>)connection.Query<GetAllSectionsAccessObject>(sqlQuery);
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
            return sections;
        }

        public List<ParagraphAccessObj> GetAllParagraphs()
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";
            string sqlQuery = "SELECT * FROM paragraph";

            var paragraphs = new List<ParagraphAccessObj>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    paragraphs = (List<ParagraphAccessObj>)connection.Query<ParagraphAccessObj>(sqlQuery);
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
            return paragraphs;
        }

        public void UpdateItem(UpdateBlogArticleAccessObj blogArticle)
        {
            if (blogArticle.HasSectionChanged) UpdateSections(blogArticle.Sections, blogArticle.HasParagraphChanged, blogArticle.HasImageChanged);
            if (blogArticle.HasTitleChanged) UpdateTitle(blogArticle);
        }
        public void UpdateTitle(UpdateBlogArticleAccessObj blogArticle)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string sqlQuery = $"UPDATE blogarticle SET title = @Title WHERE blogarticleid = @BlogId";

                    var affectedRows = connection.Execute(sqlQuery, new
                    {
                        Title = blogArticle.Title,
                        BlogArticleId = blogArticle.ArticleId
                    });
                }
                catch (Exception ex)
                {
                    throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                }
            }
        }

        public void UpdateSections(List<SectionAccessObj> sections, bool updateParagraphs, bool updateImages)
        {
            string connectionString = "UserID=postgres;Password=unearth_Anubis5;Host=localhost;Port=5432;Database=BloggyData;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    var item = SerializationManager.Serialize(sections[i].Header);
                    try
                    {
                        string sqlQuery = $"UPDATE section SET header = @Header, subheader = @Subheader WHERE sectionid = @SectionId";

                        var affectedRows = connection.Execute(sqlQuery, new
                        {
                            Header = SerializationManager.Serialize(sections[i].Header),
                            Subheader = SerializationManager.Serialize(sections[i].SubHeader),
                            SectionId = sections[i].SectionId
                        });

                        if (updateParagraphs) UpdateParagraphs(sections[i].Paragraphs);
                        if (updateImages) UpdateImages(sections[i].Images);
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
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
                    try
                    {
                        string sqlQuery = $"UPDATE paragraph SET paragraphtextarea = @ParagrahtextArea WHERE paragraphid = @ParagraphId";

                        var affectedRows = connection.Execute(sqlQuery, new
                        {
                            ParagrahtextArea = paragraphs[i].ParagraphTextArea,
                            ParagraphId = paragraphs[i].ParagraphId
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
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
                    try
                    {
                        string sqlQuery = $"UPDATE image SET bytecode = @ByteCode WHERE imageid = @ImageId";

                        var affectedRows = connection.Execute(sqlQuery, new
                        {
                            ByteCode = images[i].BytesImages,
                            ImageId = images[i].ImageId
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new GeneralDatabaseException("A Db related error occured when trying to run your query", ex);
                    }
                }
            }
        }
    }
}
