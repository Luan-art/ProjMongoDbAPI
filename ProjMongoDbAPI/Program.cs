using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using ProjMongoDbAPI.Service;
using ProjMongoDbAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProjMongoDBAPIDataBaseSettings>(
               builder.Configuration.GetSection(nameof(ProjMongoDBAPIDataBaseSettings)));

builder.Services.AddSingleton<IProjMongoDBAPIDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<ProjMongoDBAPIDataBaseSettings>>().Value);

builder.Services.AddSingleton<CustomersService>();
builder.Services.AddSingleton<AddressServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
