using System.Configuration;
using AspDotNetRazorFirst.wwwroot.entities;
using Microsoft.EntityFrameworkCore;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AspDotNetRazorFirst;

public class MovieController: DbContext
{
    protected readonly IConfiguration Configuration;

    public MovieController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));   //  System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasKey(m => m.MovieId);

        modelBuilder.Entity<Movie>().Property(m => m.MovieName).IsRequired();
    }
    

    public DbSet<Movie>? Movies { get; set; }
}