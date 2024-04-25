using Eventify.Persistence;
using Eventify.ServiceExtensions;
using System.Reflection;
using HandlersAndEvents.Event;
using Eventify.Persistence.SqlServer;
using Eventify;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
var assembly = Assembly.GetAssembly(typeof(SomeEvent))!;
string connString = builder.Configuration.GetConnectionString("Sql")!;
builder.Services.AddEventify([assembly], options => options.UseEntityFrameworkPersistence(persistentOptions => persistentOptions.UseSqlServer(connString)));

//builder.Services.AddEventify([assembly]);


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
