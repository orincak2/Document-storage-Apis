using Document_storage_Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMyDocumentStorage, InMemoryDocumentStorage>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
