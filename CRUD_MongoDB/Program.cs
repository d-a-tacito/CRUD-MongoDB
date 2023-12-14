using CRUD_MongoDB;
using CRUD_MongoDB.Models;
using CRUD_MongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MetroStoreDatabaseSettings>(
    builder.Configuration.GetSection("MetroStoreDatabase"));
builder.Services.AddSingleton<LineService>();
builder.Services.AddSingleton<StationService>();
builder.Services.AddSingleton<DeviceService>();

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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