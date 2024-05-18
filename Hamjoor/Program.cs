using Hamjoor;
using Hamjoor.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

int turnDelayDuration = 1000;
int cardCount = 10;
try
{
    int.TryParse(builder.Configuration["TurnDelayDuration"], out turnDelayDuration);
    int.TryParse(builder.Configuration["CardCount"],out cardCount);
}
catch (Exception)
{
    
}

if (turnDelayDuration < 100 || turnDelayDuration > 1000) turnDelayDuration = 1000;
if (cardCount != 8 && cardCount != 10 && cardCount != 12) cardCount = 10;

builder.Services.AddSingleton(new MatchGameModel(turnDelayDuration, cardCount));
await builder.Build().RunAsync();
