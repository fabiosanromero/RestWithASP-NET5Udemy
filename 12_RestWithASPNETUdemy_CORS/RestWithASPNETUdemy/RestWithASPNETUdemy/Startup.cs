using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => options.AddDefaultPolicy(builder =>
              {
                  builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
              }));

            services.AddControllers();

            string mysqlConnectionstr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MySQLContext>(options => options.UseMySql(mysqlConnectionstr, ServerVersion.AutoDetect(mysqlConnectionstr)));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(mysqlConnectionstr);
            }


            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                //options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").MediaType);
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").MediaType);
            })
            .AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);


            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API's from 0 to Azure with ASP.NET Core 5 and Docker",
                        Version="v1",
                        Description="API RESTFULL Develope in course",
                        Contact= new OpenApiContact
                        {
                            Name = "Fabio Romero",
                            Url=new Uri("http://github.com/fabiosanromero")
                        }
                    });
            });

            //Dependency Injections

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
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

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API's from 0 to Azure with ASP.NET Core 5 and Docker - v1");
            });

            var option =new RewriteOptions();
            app.UseRewriter(option);
            option.AddRedirect("^$", "swagger");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
        private void MigrateDatabase(string mysqlConnectionstr)
        {
            try
            {
                var evolveConnection = new MySqlConnection(mysqlConnectionstr);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations","db/dataset"},
                    IsEraseDisabled=true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database Migration failed", ex);
                throw;
            }
        }

    }
}
