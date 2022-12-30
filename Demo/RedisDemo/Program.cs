using Collapsenav.Net.Tool.Ext;
using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddFreeRedisCluster("49.235.67.56:7501", "49.235.67.56:7502", "49.235.67.56:7503", "49.235.67.56:7504", "49.235.67.56:7505", "49.235.67.56:7506");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();