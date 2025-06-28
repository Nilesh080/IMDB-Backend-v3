using System.Text;
using IMDBApi_Assignment3.Repositories;
using IMDBApi_Assignment3.Repository;
using IMDBApi_Assignment3.Repository.Interface;
using IMDBApi_Assignment3.Services;
using IMDBApi_Assignment3.Services.Interface;
using IMDBApi_Assignment3.Validations;
using IMDBApi_Assignment3.Validations.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace IMDBApi_Assignment3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:ValidIssuer"],
                    ValidAudience = Configuration["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                };
            });



            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Error;
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMDBApi", Version = "v1" });
            });
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActorValidation, ActorValidation>();
            services.AddScoped<IProducerValidation, ProducerValidation>();
            services.AddScoped<IGenreValidation, GenreValidation>();
            services.AddScoped<IReviewValidation, ReviewValidation>();
            services.AddScoped<IMovieValidation, MovieValidation>();
            services.AddScoped<IUserValidation, UserValidation>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMDBApi v1"));
            }

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