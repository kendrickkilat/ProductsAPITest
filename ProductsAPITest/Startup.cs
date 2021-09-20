using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsAPITest.Dtos;
using ProductsAPITest.Models;
using ProductsAPITest.Profiles;
using ProductsAPITest.Repositories;
using ProductsAPITest.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace ProductsAPITest
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
            services.AddDbContextPool<ProductContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductContextConnectionString")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IRepository<Product, Guid>, ProductRepository>();
            services.AddScoped<IRepository<Order, Guid>, OrderRepository>();
            services.AddScoped<IRepository<Pricing, Guid>, PricingRepository>();

            services.AddScoped<IService<OrderDto, Guid>, OrderService>();
            services.AddScoped<IService<ProductDto, Guid>, ProductService>();
            services.AddScoped<IService<PricingDto, Guid>, PricingService>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Products API", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
            });
        }
    }
}
