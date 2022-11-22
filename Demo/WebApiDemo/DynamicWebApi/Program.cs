using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDynamicWebApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseAuthentication();
// app.UseAuthorization();
app.MapControllers();
app.Run();
