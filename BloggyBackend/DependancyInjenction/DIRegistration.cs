using BloggyBackend.AutoMapper;
using Generics;
using Microsoft.Extensions.DependencyInjection;
using Repositories.BlogArticle;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggyBackend.DependancyInjenction
{
    public static class DIRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IBlogArticleRepository, BlogArticleRepository>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<BlogArticleMapping>();
            //services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(BlogArticleTransferObjProfile).Assembly);
        }
    }
}
