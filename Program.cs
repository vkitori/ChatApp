using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatApp.Data;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ChatApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Elasticsearch
var elasticsearchSettings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
    .Authentication(new BasicAuthentication("admin1", "password"))
    .ServerCertificateValidationCallback(CertificateValidations.AllowAll) // for development only
    .DefaultIndex("messages");

// Register ElasticsearchClient as a singleton
builder.Services.AddSingleton<ElasticsearchClient>(sp => new ElasticsearchClient(elasticsearchSettings));

// Register ElasticsearchService as a singleton
builder.Services.AddSingleton<ElasticsearchService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<ChatAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatAppContext") ?? throw new InvalidOperationException("Connection string 'ChatAppContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();