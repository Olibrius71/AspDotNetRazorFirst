using System.Configuration;
using AspDotNetRazorFirst.wwwroot.entities;
using AspDotNetRazorFirst.wwwroot.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace AspDotNetRazorFirst;

public class MovieContext: DbContext
{
    protected readonly IConfiguration Configuration;

    public MovieContext(IConfiguration configuration)
    {
        Configuration = configuration;
        NpgsqlConnection.GlobalTypeMapper.MapEnum<MovieType>("movie_type_enum");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasKey(m => m.MovieId);

        modelBuilder.Entity<Movie>().Property(m => m.MovieName).IsRequired();
    }
    
    public DbSet<Movie>? Movies { get; set; }
}