using Microsoft.EntityFrameworkCore;
using Tarea.MyDb.Contexts;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string conectionString = "Server=tcp:conti-db.database.windows.net,1433;Initial Catalog=conti-tienda-db;Persist Security Info=False;User ID=admin-sql;Password=2nLGnLGRJYwwie6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

builder.Services.AddDbContext<MyDbContext>(
    options => options.UseSqlServer(conectionString)
    ); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
