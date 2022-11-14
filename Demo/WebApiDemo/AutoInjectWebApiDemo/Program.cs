using Collapsenav.Net.Tool.AutoInject;
using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);
// 替换默认的工厂
builder.Host.UseAutoInjectProviderFactory();
// 必须调用 AddControllersAsServices
builder.Services.AddControllers().AddControllersAsServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
// 注册autoinject
builder.Services.AddAutoInjectController();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();