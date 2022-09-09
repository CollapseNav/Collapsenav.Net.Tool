using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;
using SimpleWebApiDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddDynamicController();
builder.Services.AddSqlitePool<EntityContext>(new SqliteConn("./Data.db"));
builder.Services.AddDefaultDbContext<EntityContext>();

// 如果业务足够简单, 就不需要实现 Controller, 偶尔会有用
builder.Services.AddQueryApi<FirstEntity, FirstGetDto>("First");
builder.Services.AddCrudApi<long?, SecondEntity, SecondCreateDto, SecondGetDto>("WTFFFFF");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
