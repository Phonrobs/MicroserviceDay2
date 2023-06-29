using Reservation.Application;
using Reservation.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddWebApiProject();
builder.AddAppplicationProject();

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

// Use Web API protection middleware
app.UseAuthentication();

app.UseAuthorization();

// Use Reservation.Application middleware
app.UseApplicationProject();

app.MapControllers();

app.Run();
