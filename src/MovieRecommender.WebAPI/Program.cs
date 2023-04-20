using MovieRecommender.Business;
using MovieRecommender.DataAccess;
using MovieRecommender.Application;
using MovieRecommender.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddConsole();

builder.Services.AddApplicationServices();
builder.Services.AddBusinessService();
builder.Services.AddDataAccessService();
builder.Services.AddHostedService<GetMovieBackgroundService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
