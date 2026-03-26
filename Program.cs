var builder = WebApplication.CreateBuilder(args);

// Configuração OBRIGATÓRIA para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Não precisa repetir a configuração de porta aqui

app.MapControllers();
app.MapDefaultControllerRoute();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();   // adicione se não tiver

app.Run();