using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDynamicWebApi(builder.Configuration.GetSection("ApiConfig").Get<ApiJsonConfig>());
builder.Services.AddDefaultSwaggerGen();

var app = builder.Build();
app.UseSwagger().UseSwaggerUI();
app.MapControllers();
app.Run();
