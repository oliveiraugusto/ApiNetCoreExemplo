using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Api.CrossCutting.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Registrando as interfaces (CrossCutting)
            ConfigureService.ConfigureDependenciesService(services);

            //Registrando Repositorios
            ConfigureRepository.ConfigureDependenciesRepository(services);


            // SWAGGER: Gerador de documentação automatizada
            // Explicação breve: https://en.wikipedia.org/wiki/Swagger_(software)
            // site do projeto: https://swagger.io/
            // Repositorio do pacote usado: https://github.com/domaindrivendev/Swashbuckle.AspNetCore


            // Swagger -- Adicionando Documentação
            services.AddSwaggerGen( c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "API com Asp Net Core (2.2)",
                            Version = "v1",
                            Description = "Exemplo de API com Asp .Net Core",
                            Contact = new OpenApiContact
                            {
                                Name = "Cesar Oliveira Jr",
                                Email = "oliveiraugusto@ymail.com",
                                Url = new Uri("http://github.com/oliveiraugusto")
                            }
                        }
                    );
                }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //Swagger - Ativando middlewares
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = String.Empty;
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exemplo de API com Asp .Net Core 2.2");
                }
            );

            //Swagger - Redirecionando rota principal (http://localhost:5000) para a rota do swagger (http://localhost:5000/swagger)
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);


            app.UseMvc();
        }
    }
}
