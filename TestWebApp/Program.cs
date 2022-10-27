using Microsoft.EntityFrameworkCore;
using TestWebApp;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddScoped<DbInitializer>()
    .AddDbContext<BlogContext>(options =>
    {
        options.UseNpgsql("Host=localhost;Database=test");
    });

builder.WebHost.UseSentry(options =>
{
    options.Dsn = "https://eb18e953812b41c3aeb042e666fd3b5c@o447951.ingest.sentry.io/5428537";
    options.Debug = true;
    options.TracesSampleRate = 1.0;
});

var app = builder.Build();
app.UseSentryTracing();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitAsync();
}

app.MapGet("/", async (BlogContext ctx) =>
{
    return await ctx.Blogs.Where(b => b.Name.StartsWith("F")).ToListAsync();
});

app.Run();
