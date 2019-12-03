using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetCore3.Template.Api
{
    /// <summary>
    /// Application configuration start up.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Application settings configuration property.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme);
            // TODO: Setup options and move values into config
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = Configuration["Authorization:Authority"];
            //    options.ApiName = AuthorizationConstants.ApiName;
            //    options.EnableCaching = true;
            //    options.CacheDuration = TimeSpan.FromHours(1);
            //    options.NameClaimType = "sub";
            //    options.RoleClaimType = "role";
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = ".NET Core 3.0 Template API",
                    Description = "An API to use as a template for future migrations to .NET Core 3.0."
                });

                // Ensure the routes are added to the right Swagger doc
                c.DocInclusionPredicate((version, desc) =>
                {
                    var versionModel = desc.ActionDescriptor.GetApiVersionModel();

                    var checkIfControllerSupportsCurrentVersion = versionModel.SupportedApiVersions.Any(v => $"v{v.ToString()}" == version);

                    var checkIfActionIsInCurrentVersion = versionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == version);

                    return checkIfControllerSupportsCurrentVersion && checkIfActionIsInCurrentVersion;
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", ".NET Core 3.0 Template API V1.0");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
