using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddSqlitePool<EntityContext>(new SqliteConn("./Data.db"));
builder.Services.AddDefaultDbContext<EntityContext>();
builder.Services.AddRepository();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
