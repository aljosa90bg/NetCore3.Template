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
using System.Collections.Generic;
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

        // This method gets called by the runtime. Use this method to add services to the container.
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
                    Title = "Svea Authentication User Store API",
                    Description = "An API for authentication user store access and management."
                });

                // Test second version
                //c.SwaggerDoc("v2.0", new OpenApiInfo
                //{
                //    Version = "v2.0",
                //    Title = "Svea Authentication User Store API",
                //    Description = "An API for authentication user store access and management."
                //});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

                // TODO: Setup filters
                //c.OperationFilter<RemoveVersionFromParameter>();
                //c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Svea Authentication User Store API V1.0");
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
