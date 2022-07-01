using LibraryProject.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), new[] {"Liveness"});
    
string mongoConnectionString=builder.Configuration.GetConnectionString("UserDb");
// Add services to the container.
builder.Services.AddScoped<ILibraryDataStore, LibraryDataStore>();
builder.Services.AddControllers();

var mongoUrl = MongoUrl.Create(mongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoUrl));
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
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/live", new HealthCheckOptions { Predicate = p => p.Tags.Contains("Liveness") });
    endpoints.MapHealthChecks("/ready", new HealthCheckOptions { Predicate = p => p.Tags.Contains("Readiness") });
});
app.MapControllers();

app.Run();
