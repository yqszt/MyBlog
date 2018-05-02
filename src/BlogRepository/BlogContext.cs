using BlogModel;
using Microsoft.EntityFrameworkCore;

namespace BlogRepository
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
    }
}
