using Microsoft.EntityFrameworkCore;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.ApplicationLayer.Services;
using PreTrainee_Month2.CoreLayer.Product_Entities;
using PreTrainee_Month2.CoreLayer.Repository_Interfaces;
using PreTrainee_Month2.InfrastructureLayer.DataBaseContext;
using PreTrainee_Month2.InfrastructureLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IProductService,ProductService>();
builder.Services.AddTransient<IUserService,UserService>();
builder.Services.AddDbContext<DBContext>(
    options =>
    {
        options.UseSqlServer(@"Server= mssqlserver_container;Database=PreTraineeMonth2_DB;Trusted_Connection=true;"
        , b => b.MigrationsAssembly("PreTrainee_Month2"));
    });
var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
