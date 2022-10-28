using EasyApiService.StartupServices.AppService;
using EasyApiService.StartupServices.AuthenticationServices;
using EasyApiService.StartupServices.CorsServices;
using EasyApiService.StartupServices.ElmahServices;
using EasyApiService.StartupServices.SeedService;
using EasyApiService.StartupServices.SqlServerService;
using ElmahCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RunAppServices();
builder.Services.RunElmahSqlService(builder.Configuration);
builder.Services.RunSqlServerService(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.RunCorsService();
builder.Services.AddJwtAuthenticationService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.RunInitialScopeService();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseElmah();
app.Run();