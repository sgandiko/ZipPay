using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZipPay.API.Mapper;
using ZipPay.Domain.Interface.Repository.UserAccountRepo;
using ZipPay.Domain.Interface.Repository.UserRepo;
using ZipPay.Domain.Interface.Service;
using ZipPay.Infrastructure.Data;
using ZipPay.Infrastructure.Persistence.Repository.UserAccountRepo;
using ZipPay.Infrastructure.Persistence.Repository.UserRepo;
using ZipPay.Infrastructure.service;

namespace ZipPay.API
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
            services.AddDbContext<ZipPayDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ZipPayUserConnection")));
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            //Dependency Injection of Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountRepository>();

            //Dependency Injection of Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserAccountService, UserAccountService>();

            //AutoMapper Configuration

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = new RewriteOptions().AddRedirectToHttps(StatusCodes.Status301MovedPermanently);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();

                
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
