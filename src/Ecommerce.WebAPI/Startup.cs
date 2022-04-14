using Ecommerce.WebAPI.Data;
using Ecommerce.WebAPI.Queries;
using Ecommerce.WebAPI.Queries.Clientes;
using Ecommerce.WebAPI.Queries.Pedidos;
using Ecommerce.WebAPI.Queries.Produtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Ecommerce.WebAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce.WebAPI", Version = "v1" });
            });

            services.AddDbContext<Context>(opt =>
                opt.UseMySQL(Configuration.GetConnectionString("sqlConnection")));

            services.AddScoped<ISqlConnectionAccessor, MySqlConnectionAcessor>();
            services.AddScoped<IClientesQuery, ClientesQuery>();
            services.AddScoped<IProdutosQuery, ProdutosQuery>();
            services.AddScoped<IPedidoQuery, PedidoQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
