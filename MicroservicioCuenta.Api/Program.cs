using MicroservicioCuenta.Api.Repositories;
using MicroservicioCuenta.Api.Services;
using MicroservicioCuenta.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICuentaRepository, CuentaRepository>(); 
builder.Services.AddScoped<ICuentaService, CuentaService>();  


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();  
}


app.UseRouting();
app.MapControllers();

app.Run();
