using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{

    // Uncomment one of the two lines depending on the identity type
    var credential = new DefaultAzureCredential(); // system-assigned identity
                                                   
    //var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = '<client-id-of-user-assigned-identity>' }); // user-assigned identity

    // Get token for Azure Database for MySQL
    var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://ossrdbms-aad.database.windows.net" }));

    // Set MySQL user depending on the environment
    string user = "mysql-v2-playground-admin@mysql-mhv2-m4hf2peroh2ja";

    // Add the token to the MySQL connection
    var connectionString = "Server=mysql-mhv2-m4hf2peroh2ja.mysql.database.azure.com;" +
        "Port=3306;" +
        "SslMode=Required;" +
        "Database=patientapis;" +
        "Uid=" + user + ";" +
        "Password=" + token.Token;

    var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

    connection.Open();
    var isConnected = connection.Ping();

    Console.WriteLine(isConnected);

    return $"{isConnected}";
});

app.Run();
