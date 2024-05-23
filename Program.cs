using Blazored.Toast;
using CursoBlazorWebAssembly;
using CursoBlazorWebAssembly.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//conectamos con la api guardada en el appsettings
var apiUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
//libreria
builder.Services.AddBlazoredToast();
//inyeccion de dependencias
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
