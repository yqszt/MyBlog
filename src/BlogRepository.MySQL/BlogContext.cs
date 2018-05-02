using BlogModel;
using Microsoft.EntityFrameworkCore;

namespace BlogRepository.MySQL
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
    }
}
