using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspDotNetRazorFirst.wwwroot.entities;

[Table("movies")]
public class Movie
{
    
    [Column("movie_id")]
    public int MovieId { get; set; }
    
    [Column("movie_name")]
    public string MovieName { get; set; }
    
}