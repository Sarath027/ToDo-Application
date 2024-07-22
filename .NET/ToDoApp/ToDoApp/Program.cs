
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoApp.Repository;
using ToDoApp.Repository.Interfaces;
using ToDoApp.Services;
using ToDoApp.Services.Interfaces;

namespace ToDoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = true;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtIssuer,
                     IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                 };
             });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<ITodoServiceInterface,TaskService>();
            builder.Services.AddScoped<ITodoRepositoryInterface, TaskRepository>();
            builder.Services.AddTransient<IUserServiceInterface, UserService>();
            builder.Services.AddScoped<IUserRepositoryInterface, UserRepository>();
            
            builder.Services.AddDbContext<ToDoAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("database")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
           
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
