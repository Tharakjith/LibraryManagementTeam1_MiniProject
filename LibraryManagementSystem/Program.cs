using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            //Register a JWT authentication schema
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

                .AddJwtBearer(opt =>
     {
         opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                 builder.Configuration["Jwt:Key"]))
         };
     });


            //3-json format
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(
             options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 options.JsonSerializerOptions.ReferenceHandler =
                 System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 options.JsonSerializerOptions.DefaultIgnoreCondition =
                 System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                 options.JsonSerializerOptions.WriteIndented = true;
             });


            // Configure database context
            builder.Services.AddDbContext<LibraryMngtDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));

            


            builder.Services.AddScoped<IBorrowTransactionRepository, BorrowTransactionRepository>();

            // 1- connection string as middleware
            builder.Services.AddDbContext<LibraryMngtDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug2024Connection")));

            
            builder.Services.AddScoped<IMembersCategoryRepository, MembersCategoryRepository>();
            
            //Before app, 


            //2- Register Repository and service layer
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IBorrowTransactionRepository, BorrowTransactionRepository>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
