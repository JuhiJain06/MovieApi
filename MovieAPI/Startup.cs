using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieAPI.Helpers;
using MovieAPI.Helpers.Interface;
using MovieAPI.Middleware;
using MovieAPI.Models.DTOs;
using MovieAPI.Models.Settings;
using MovieAPI.Repositories;
using MovieAPI.Repositories.Interfaces;
using MovieAPI.Services;
using MovieAPI.Services.Interfaces;
using System.Text;

namespace MovieAPI
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
                }
             );
            
            services.AddAuthorization();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI", Version = "v1" });
            });


            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
           services.Configure<SupabaseSetting>(Configuration.GetSection("Supabase"));

            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISupabaseService, SupabaseService>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<IActorsRepository, ActorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMoviesRepository, MovieRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieAPI v1"));
            }

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();//handles exceptions

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
