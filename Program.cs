using CursoBlazorWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//conectamos con la api guardada en el appsettings
var apiUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

await builder.Build().RunAsync();
