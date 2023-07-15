using BlazorCommunication.Client;
using BlazorCommunication.Client.Services;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CustomHttpInterceptor>();
builder.Services.AddHttpClient(HttpClientNames.BlazorCommunicationAPI, client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>(HttpClientNames.BlazorCommunicationAPI));
}).AddHttpMessageHandler<CustomHttpInterceptor>();

builder.Services.AddHttpClient(HttpClientNames.BlazorIdentityAPI, client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>(HttpClientNames.BlazorIdentityAPI));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
