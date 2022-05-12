using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                                                {
                                                    Description = "Standard Authorization header using Bearer scheme (\"bearer {token}\")",
                                                    In = ParameterLocation.Header,
                                                    Name = "Authorization",
                                                    Type = SecuritySchemeType.ApiKey
                                                });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
builder.Services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(
           options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
                                                   {
                                                       ValidateIssuerSigningKey = true,
                                                       IssuerSigningKey = new SymmetricSecurityKey(
                                                           Encoding.UTF8.GetBytes(
                                                               builder.Configuration.GetSection("AppSettings:Token").Value)),
                                                       ValidateIssuer = false,
                                                       ValidateAudience = false
                                                   };
           });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();