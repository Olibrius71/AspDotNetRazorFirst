using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using AspDotNetRazorFirst.wwwroot.enums;
using Microsoft.VisualBasic;

namespace AspDotNetRazorFirst;

public class TasteDiveWebScraper : WebScraper
{
    
    private List<IElement> cardsContainer { get; set; }
    
    public TasteDiveWebScraper(string movieName, string movieType)
    {
        SearchUrl = "https://tastedive.com/";
        
        string movieStringLink = "";
        string[] movieWords = movieName.Split(" ");
        foreach (var word in movieWords)
        {
            string finalWord = Regex.Replace(word, @"[#&,$+¤£;:]", "");
            movieStringLink += (finalWord=="-") ? "" : char.ToUpper(finalWord[0]) + finalWord.Substring(1);
            if (Array.IndexOf(movieWords, word) != movieWords.Length - 1)
            {
                movieStringLink += (finalWord == "" || finalWord == "-") ? "" : "-";
            }
        }
        if (movieType == MovieType.Movie.ToString())
        {
            SearchUrl += "movies/like/" + movieStringLink;
            if (movieWords.Length == 1)
            {
                SearchUrl += "-Movie";
            }
        }
        else
        {
            SearchUrl += "shows/like/" + movieStringLink + "-TV-Show";
            if (movieWords.Length == 1)
            {
                SearchUrl += "-TV-Show";
            }
        }
    }

    public void InitializeCards()
    {
        Console.WriteLine(SearchUrl);
        Console.WriteLine(document.Body.QuerySelectorAll("h2").First().TextContent + document.Body.QuerySelectorAll("h2").Skip(1).First().TextContent);
        cardsContainer = document.Body.QuerySelectorAll("h2").Skip(1).First()
            .ParentElement
            .NextElementSibling
            .NextElementSibling
            .Children.ElementAt(2)
            .Children.ToList();
    }
    
    public List<string> GetNewTitles()
    {
        List<string> newTitles = new List<string>();
        foreach (var overallCardContainer in cardsContainer)
        {
            var AElementContainer = overallCardContainer.Children.ElementAt(3).Children.ElementAt(0).Children.ElementAt(0);
            newTitles.Add(AElementContainer.Children.ElementAt(1).Children.ElementAt(1).TextContent);
        }
        return newTitles;
    }

    public List<string> GetNewDates()
    {
        List<string> newTitles = new List<string>();
        foreach (var overallCardContainer in cardsContainer)
        {
            var AElementContainer = overallCardContainer.Children.ElementAt(3).Children.ElementAt(0).Children.ElementAt(0);
            newTitles.Add(AElementContainer.Children.ElementAt(1).Children.ElementAt(2).TextContent);
        }
        return newTitles;
    }

    public List<string> GetNewDescs()
    {
        List<string> newTitles = new List<string>();
        foreach (var overallCardContainer in cardsContainer)
        {
            var AElementContainer = overallCardContainer.Children.ElementAt(3).Children.ElementAt(0).Children.ElementAt(0);
            newTitles.Add(AElementContainer.Children.ElementAt(1).Children.ElementAt(3).TextContent);
        }
        return newTitles;
    }

    public async Task<byte[]> GetNewImageWithIndex(int movieIndex)
    {
        var AElementContainer = cardsContainer[movieIndex].Children.ElementAt(3).Children.ElementAt(0).Children.ElementAt(0);
        string imageSrc = AElementContainer.Children.ElementAt(0).Children.ElementAt(0).Children.ElementAt(0)
            .Attributes["src"].Value;
        
        var imageBytes = await _httpClient.GetByteArrayAsync(imageSrc);
        using (var memoryStream = new MemoryStream())
        {
            await memoryStream.WriteAsync(imageBytes,0,imageBytes.Length);
            return memoryStream.ToArray();
        }
    }
}