using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TorteLand;
using TorteLand.PostgreSql.Models;
using TorteLand.WebAPI2.Auth;
using Article = TorteLand.Article;
using Articles = TorteLand.Articles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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
                                                               builder.Configuration.GetSection("Auth:Token").Value)),
                                                       ValidateIssuer = false,
                                                       ValidateAudience = false
                                                   };
           });

builder.Services
       .AddSingleton<IAuth>(new Auth(
                                builder.Configuration.GetSection("Auth:Token").Value,
                                Encoding.UTF8))
       .AddScoped<ICrudl<int, Article>, TorteLand.PostgreSql.Articles>()
       .AddScoped<IArticles, Articles>();

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