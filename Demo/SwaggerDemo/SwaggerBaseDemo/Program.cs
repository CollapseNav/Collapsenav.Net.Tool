using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
// 添加带注释的swagger, 需要修改csproj文件以自动生成xml文档
services.AddSwaggerWithComments();
// 添加带 jwt Authorize 的 swagger 
services.AddSwaggerWithJwtAuth();
// 提供一个默认的swagger, 包含以上的注释和jwt Authorize
// services.AddDefaultSwaggerGen();


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
