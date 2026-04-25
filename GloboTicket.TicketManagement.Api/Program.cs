using GloboTicket.TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

if (!app.Environment.IsEnvironment("Testing"))
{
    await app.ResetDatabaseAsync();
}

app.Run();

public partial class Program { };
