using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Bloggy.Auth
{
    public class Startup
    {
        private string Environment => System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
              .AddCookie()
              .AddOpenIdConnect("Auth0", options => {

                  options.Authority = $"https://{Configuration["Auth0:Domain"]}";
                  options.ClientId = Configuration[$"{Environment}:Security:ClientId"];
                  options.ClientSecret = Configuration[$"{Environment}:Security:ClientSecret"];
                  options.ResponseType = OpenIdConnectResponseType.Code;
                  options.Scope.Add("openid");
                  options.CallbackPath = new PathString("/Account/callback");
                  options.ClaimsIssuer = "Auth0";

                  options.Events = new OpenIdConnectEvents
                  {
                      OnRedirectToIdentityProviderForSignOut = (context) =>
                      {
                          context.ProtocolMessage.SetParameter("audience", Configuration[$"{Environment}:HostDomain"]);

                          var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration[$"{Environment}:Security:ClientId"]}";

                          var postLogoutUri = context.Properties.RedirectUri;
                          if (!string.IsNullOrEmpty(postLogoutUri))
                          {
                              if (postLogoutUri.StartsWith("/"))
                              {
                                  var request = context.Request;
                                  postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                              }
                              logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri) }";
                          }

                          context.Response.Redirect(logoutUri);
                          context.HandleResponse();

                          return Task.CompletedTask;
                      }
                  };
              });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
