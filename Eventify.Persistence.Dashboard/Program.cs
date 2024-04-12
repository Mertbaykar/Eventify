using Eventify.Persistence;
using Eventify.Persistence.Dashboard.Components;
using Eventify.Persistence.SqlServer;
using HandlersAndEvents.Event;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(config => config.DetailedErrors = true);


// Add services to the container.
var assembly = Assembly.GetAssembly(typeof(SomeEvent))!;
string connString = builder.Configuration.GetConnectionString("Sql")!;
builder.Services.AddEventify([assembly], options => options.UseEntityFrameworkPersistence(persistentOptions => persistentOptions.UseSqlServer(connString, false)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
