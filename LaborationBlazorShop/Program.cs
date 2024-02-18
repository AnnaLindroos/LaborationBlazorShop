using LaborationBlazorShop.Client.Pages;
using LaborationBlazorShop.Components;
using LaborationBlazorShop.Components.Account;
using LaborationBlazorShop.Data;
using LaborationBlazorShop.Models;
using LaborationBlazorShop.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var connectionString = builder.Configuration.GetConnectionString("BlazorLabDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ProductService>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    AddData(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LaborationBlazorShop.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

void AddData(ApplicationDbContext db)
{
    if (!db.Products.Any())
    {
        db.AddRange(
        new Product
        {
            Title = "Pro-Flashlight",
            Description = "With an extra battery life and even brighter bulb, your colleagues can never leave you in the dark again!",
            Image = "https://cdna.artstation.com/p/assets/images/images/070/917/896/large/danylo-zhelezniak-tbrender-003.jpg?1703946726",
            Price = 30,
            OriginalPrice = 30,
            Stock = 2
        },
        new Product
        {
            Title = "Inverse Teleporter",
            Description = "The inverse teleporter is a modified teleporter which will teleport you to a random position outside the ship. All your items will be dropped at the teleporter before transport. The inverse teleporter can be used by everyone at once and has a 3.5 minute cooldown.\r\nDISCLAIMER: The inverse teleporter can only transport you out, not in, and you may become trapped. The Company is not responsible for injury or replacement of heads and limbs induced by quantum entanglement and bad luck.",
            Image = "https://gcdn.thunderstore.io/live/repository/icons/PortableNavi-BetterInverseTeleporter-1.0.2.png.256x256_q95.png",
            Price = 400,
            OriginalPrice = 425,
            Stock = 1
        },
        new Product
        {
            Title = "Loud Horn",
            Description = "Used to communicate with any crew member from any distance, no walkie talkie required! The horn can be heard from anywhere. But what does it mean? That’s up to you!",
            Image = "https://www.tierlista.com/wp-content/uploads/2023/11/Lethal-Company-Shops-Items-16.webp",
            Price = 150,
            OriginalPrice = 150,
            Stock = 0
        },
        new Product
        {
            Title = "Zap Gun",
            Description = "The most specialized self-protective equipment, capable of sending upwards of 80,000 volts!\r\nTo Keep it targeted as long as possible, pull the gun side-to-side to counter the bend and fightagainst the pull of the electric beam. It can only stun for as long as you keep the current flowing",
            Image = "https://staticg.sportskeeda.com/editor/2023/11/ab263-17010769096844-1920.jpg?w=840",
            Price = 400,
            OriginalPrice = 400,
            Stock = 4
        },
        new Product
        {
            Title = "TZP-Inhalant",
            Description = "This safe and legal medicine can be administered to see great benefits to your performance on the job! Your ability to move LONG distances while carrying HEFTY objects will be second to none! \r\nWarning: TZP gas may impact the brain with extended exposure. Follow instructions manual provided with the canister.\r\nDon’t forget to share!",
            Image = "https://media.thenerdstash.com/wp-content/uploads/2023/11/Lethal-Company-TZP-Inhalant.jpg",
            Price = 100,
            OriginalPrice = 120,
            Stock = 2
        });
        db.SaveChanges();
    }
}
