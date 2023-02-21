using System.ComponentModel.DataAnnotations;

namespace AspDotNetRazorFirst.wwwroot.enums;


public enum MovieType
{
    [Display(Name = "Movie")]
    Movie,
    [Display(Name = "Series")]
    Series
}