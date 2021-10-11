using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //ConfigureServices SERVE PARA ADICIONAR OS SERVIÇOS
        public void ConfigureServices(IServiceCollection services)
        {
            //Adiciona o serviço MVC
            services.AddMvc();

            //esse serviço mantém informações na mémória
            services.AddDistributedMemoryCache();

            //adicionar uma sessão
            services.AddSession();

            //Pagando a configuração do banco de dados
            string connectionString = Configuration.GetConnectionString("Default");
            
            //adicionar o contexto do banco de dados
            services.AddDbContext<ApplicationContext>(options =>
                //Usando a configuração para conectar o banco de dados
                options.UseSqlServer(connectionString)
            );

            //adicionando um instancia temporária
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<ICadastroPedidoRepository, CadastroPedidoRepository>();
            services.AddTransient<IItemPedidoRepository, ItemPedidoRepository>();
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure serve para consumir os serviços
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",//definindo o carrossel como rota padrão
                    template: "{controller=Pedido}/{action=Carrossel}/{codigo?}");
            });
            //Criando banco de dados
            serviceProvider.GetService<IDataService>().IncializaDB();
        }
    }
}
