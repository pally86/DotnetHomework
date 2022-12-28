
using DotnetHomework.Data;
using DotnetHomework.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentsRepository, DocumentsRepository>();
builder.Services.AddScoped<IStorageFactory, StorageFactory>();
builder.Services.AddScoped<IStorage, StorageCloud>();
builder.Services.AddScoped<IStorage, StorageHDD>();
builder.Services.AddScoped<IConverter, Converter>();


builder.Services
  .AddMvc(options => 
  {
      options.SuppressAsyncSuffixInActionNames = false;  
  }).AddXmlSerializerFormatters(); ;



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
