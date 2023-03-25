using Microsoft.EntityFrameworkCore;

namespace NewsPage.Models
{
    public class NewsPageContext : DbContext
    {
        public NewsPageContext(DbContextOptions<NewsPageContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Image> Images { get; set; }
        public string WebRootPath { get; internal set; }
    }
}
