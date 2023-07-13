using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product_API.Data;
using Product_API.Models;
using Product_API.Services;

namespace Product_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MyDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("MyDB"));
            });
            //builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
            builder.Services.AddScoped<ITypeRepository, TypeRepositoryInMemory>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));

            var secretKey = builder.Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        // tu cap token
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // ky vao token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero
                    };
                }
                );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}