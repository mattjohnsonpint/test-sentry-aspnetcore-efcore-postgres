namespace TestWebApp;

public class DbInitializer
{
    private readonly BlogContext _context;

    public DbInitializer(BlogContext context)
    {
        _context = context;
    }

    public async Task InitAsync()
    {
        // Destroy and rebuild the db every time the app is run
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
        
        _context.Blogs.Add(new() {Name = "Foo Blog 1"});
        _context.Blogs.Add(new() {Name = "Foo Blog 2"});
        _context.Blogs.Add(new() {Name = "Foo Blog 3"});
        _context.Blogs.Add(new() {Name = "Bar Blog 4"});
        _context.Blogs.Add(new() {Name = "Bar Blog 5"});
        _context.Blogs.Add(new() {Name = "Bar Blog 6"});
        await _context.SaveChangesAsync();
    }
}