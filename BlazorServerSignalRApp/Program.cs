using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorServerSignalRApp.Data;
using Microsoft.AspNetCore.ResponseCompression;
using BlazorServerSignalRApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

TranslationService? translationService;
if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
{
    translationService = new MockTranslationService();
}
else
{
    translationService = new GoogleTranslationService();
}
builder.Services.AddSingleton<TranslationService>(translationService);

var apiKey = Environment.GetEnvironmentVariable("STIPOP_APIKEY");
if (String.IsNullOrEmpty(apiKey)) throw new InvalidOperationException("Server cannot run without API key of Stipop");
StickerService stickerService = new StickerService(apiKey);
builder.Services.AddSingleton<StickerService>(stickerService);

builder.Services.AddSingleton<ChannelService>();

var app = builder.Build();
app.UseResponseCompression();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
