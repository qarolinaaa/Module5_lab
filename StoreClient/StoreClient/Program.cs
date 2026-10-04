using StoreClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("StoreApi", client =>
{
    client.BaseAddress = new Uri(
        "https://localhost:7168/");
});

builder.Services.AddScoped<
    IStoreProductApiService,
    StoreProductApiService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StoreProduct}/{action=Index}/{id?}");

app.Run();
