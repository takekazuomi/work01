using Azure.Identity;
using Azure.Core.Diagnostics;

// logging
using AzureEventSourceListener listener = AzureEventSourceListener.CreateConsoleLogger();

// Uncomment one of the two lines depending on the identity type
//var credential = new DefaultAzureCredential(); // system-assigned identity
// 752adf6e-e235-452c-86de-612ac5964fbb rg-mhv2-playground-capps-backend/id-mhv2-backend
// 5387bae1-8091-49d8-9666-c713e421ab7b rg-mhv2-playground-apps/id-mhv2-appservice
var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "5387bae1-8091-49d8-9666-c713e421ab7b" }); // user-assigned identity

// Get token for Azure Database for MySQL
var token = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://ossrdbms-aad.database.windows.net" }));

// Set MySQL user depending on the environment
string user = "mhv2-appservice@mysql-mhv2-m4hf2peroh2ja";

// Add the token to the MySQL connection
var connectionString = "Server=mysql-mhv2-m4hf2peroh2ja.mysql.database.azure.com;" + 
    "Port=3306;" + 
    "SslMode=Required;" +
    "Database=patientapis;" + 
    "Uid=" + user+ ";" +
    "Password="+ token.Token;

var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

connection.Open();

Console.WriteLine(connection.Ping());

