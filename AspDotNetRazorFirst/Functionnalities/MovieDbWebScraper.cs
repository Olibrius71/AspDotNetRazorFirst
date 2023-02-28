using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Scripting;
using AspDotNetRazorFirst.wwwroot.enums;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using IConfiguration = AngleSharp.IConfiguration;


namespace AspDotNetRazorFirst;

public class MovieDbWebScraper : WebScraper
{
    
    public MovieDbWebScraper(string movieName)
    {
        SearchUrl = "https://www.themoviedb.org/search?language=fr&query=";
        
        string[] movieWords = movieName.Split(" ");
        foreach (var word in movieWords)
        {
            string finalWord = Regex.Replace(word, @"[#&,$+¤£;:]", "");
            SearchUrl += finalWord;
            if (Array.IndexOf(movieWords, word) != movieWords.Length - 1)
            {
                SearchUrl += "+";
            }
        }
    }

    public string GetDate()
    { 
        var cells = document.QuerySelectorAll(".results.flex > .card .release_date");
        string date = cells.Select(m => m.TextContent).ToList().First();

        return date;
    }

    public string GetTitle()
    {
        var cells = document.QuerySelectorAll(".results.flex > .card .details > .wrapper > .title > div > a.result > h2");
        string title = cells.Select(m => m.TextContent).ToList().First();

        return title;
    }

    public string GetDescription()
    {
        var cells = document.QuerySelectorAll(".results.flex > .card .details > .overview > p");
        string description = cells.Select(m => m.TextContent).ToList().First();

        return description;
    }

    private string TransformImageSize(string src)
    {
        int indexWidth = src.IndexOf("w");
        src = src.Remove(indexWidth + 1, 2).Insert(indexWidth + 1, "188");
        int indexHeight = src.IndexOf("h");
        src = src.Remove(indexHeight + 1, 3).Insert(indexHeight + 1, "282");

        return src;
    }
    
    public async Task<byte[]> GetImage()
    {
        var cells = document.QuerySelectorAll(".results.flex > .card img");
        string src = cells.Select(m => m.Attributes["src"].Value).ToList().First();

        src = TransformImageSize(src);  // L'image est tout le temps format 94x141 (et en changeant l'attribut src on peut l'agrandir)
        
        string finalstring = "https://www.themoviedb.org" + src;
        
        var imageBytes = await _httpClient.GetByteArrayAsync(finalstring);
        using (var memoryStream = new MemoryStream())
        {
            await memoryStream.WriteAsync(imageBytes,0,imageBytes.Length);
            return memoryStream.ToArray();
        }
    }

    
    public string GetType()
    {
        var cells = document.QuerySelectorAll("#search_menu_scroller > ul > li");
        var selectedCategory = cells.First(categ => categ.ClassList.Contains("selected"));
             
        string selectedCategoryName = selectedCategory.ChildNodes.OfType<IElement>().Select(m => m.TextContent).First();
        
        
        switch (selectedCategoryName)
        {
            case "Films":
                return MovieType.Movie.ToString();
            case "Émissions télévisées":
                return MovieType.Series.ToString();
            default:
                throw new Exception("Erreur pour trouver le type, selectedcatname= "+ selectedCategoryName);
        }
    }
}