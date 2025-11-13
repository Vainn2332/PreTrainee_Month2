using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PreTrainee_Month2.ApplicationLayer.MiddleWares;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.ApplicationLayer.Services;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;
using PreTrainee_Month2.InfrastructureLayer.DataBaseContext;
using PreTrainee_Month2.InfrastructureLayer.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateIssuer = true,
        ValidIssuer=AuthOptions.ISSUER,
        ValidateLifetime=true,
        //секретный ключ    
        IssuerSigningKey=AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey=true
    });

builder.Services.AddAuthorization();

builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IProductService,ProductService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddDbContext<DBContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DBContext)));
       
    });
var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseMiddleware<ExceptionHandlerMiddleWare>();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
