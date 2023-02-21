using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspDotNetRazorFirst.wwwroot.entities;

[Table("movies")]
public class Movie
{
    
    [Column("movie_id")]
    public int MovieId { get; set; }
    
    [Column("movie_name")]
    [MinLength(4)]
    public string MovieName { get; set; }
    
    [Column("movie_date")]
    [DataType(DataType.DateTime)]
    public DateTime MovieDate { get; set; }
    
    [Column("movie_description")]
    public string? MovieDesc { get; set; } 
    
    [Column("movie_image_data")]
    public byte[]? MovieImageData { get; set; }
    
    [Column("movie_type")]
    public string? MovieType { get; set; }
    
}