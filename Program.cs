
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reisinger_Intelliface_1_0.Data;
using Reisinger_Intelliface_1_0.FaceRecognition;
using Reisinger_Intelliface_1_0.Services;
using Reisinger_Intelliface_1_0.Storage;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EmployeeDB");






// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContextFactory<IntellifaceDataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDbContext<IntellifaceDataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<IntellifaceDataContext>();


builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BasicRepository<>));
builder.Services.AddScoped<SeventhSenthService>();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddTransient<IFaceRecognizer, SeventhSenthService>();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    //builder.AddFile();
});

//builder.Services.AddScoped<IFaceRecognizer, SeventhSenthSer
//

//vice>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IntellifaceDataContext>();
    dbContext.Database.EnsureCreated();
}

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
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
