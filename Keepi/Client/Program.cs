using Keepi.Client;
using Keepi.Client.Communication;
using Keepi.Client.Repositories.Implementation;
using Keepi.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IHttpService, HttpService>();

builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();


await builder.Build().RunAsync();
