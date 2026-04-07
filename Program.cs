using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// pega connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// testa conexão
using (var connection = new MySqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Conectado ao banco 🚀");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao conectar: " + ex.Message);
    }
}

var app = builder.Build();

app.MapGet("/", () => "API rodando");

app.Run();