using System.Text;
using Chatrooms.Web.Api.Options;
using Chatrooms.Web.Api.Data;
using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Helpers;
using Chatrooms.Web.Api.Logic;
using Chatrooms.Web.Api.Logic.Factories;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Chatrooms.Web.Api
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
            services.AddDbContext<ChatroomsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ChatroomsDb"));
            });

            services.Configure<JwtOptions>(Configuration.GetSection("Tokens"));

            services.AddIdentity<User, Role>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ChatroomsDbContext>()
                .AddDefaultTokenProviders();

            ConfigureJwtBearer(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IAuthenticationLogic, AuthenticationLogic>();
            services.AddTransient<IUsersLogic, UsersLogic>();
            services.AddTransient<IUserFactory, UserFactory>();

            services.AddTransient<JwtTokenHelper>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureJwtBearer(IServiceCollection services)
        {
            var config = Configuration.GetSection("Tokens").Get<JwtOptions>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = config.Issuer,
                        ValidAudience = config.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key))
                    };
                });
        }
    }
}