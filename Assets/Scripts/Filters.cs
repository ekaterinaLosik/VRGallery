using System;
using System.Collections.Generic;
using System.Linq;

public static class Filters
{
    public static List<Artwork> SelectedArtworkslist;

    public static string[] GetAllTags(Artwork[] artworks)
    {
        List<string> artworklist = new List<string>();
        foreach (Artwork artwork in artworks)
        {
            foreach (string tag in artwork.tags)
            {
                if(!artworklist.Contains(tag.ToLower())) artworklist.Add(tag.ToLower());
            }
        }
        return artworklist.ToArray();
    }

    public static string[] GetAllAuthors(Artwork[] artworks)
    {
        List<string> artworklist = new List<string>();
        foreach (Artwork artwork in artworks)
        {
           if(!artworklist.Contains(artwork.author)) artworklist.Add(artwork.author);
        }
        return artworklist.ToArray();
    }

    public static List<string> GetAllYears(Artwork[] artworks)
    {
        List<string> artworklist = new List<string>();
        foreach (Artwork artwork in artworks)
        {
            if(!artworklist.Contains(artwork.year.ToString())) artworklist.Add(artwork.year.ToString());
        }
        return artworklist;
    }


    public static Artwork[] FilterForTag(string tag, Artwork[] artworks) // Requires all tags in artwork to be lowercase
    {
        List<Artwork> artworklist = new List<Artwork>();
        string ltag = tag.ToLower();
        foreach (Artwork artwork in artworks)
        {
            if(Array.IndexOf(artwork.tags, ltag) > -1) artworklist.Add(artwork);
        }
        return artworklist.ToArray();
    }

    public static Artwork[] FilterForAuthor(string author, Artwork[] artworks)
    {
        List<Artwork> artworklist = new List<Artwork>();
        foreach (Artwork artwork in artworks)
        {
            if (artwork.author.ToLower() == author.ToLower()) artworklist.Add(artwork);
        }
        return artworklist.ToArray();
    }

    public static Artwork[] FilterForCv(string author, Artwork[] artworks)
    {
        Artwork[] ArtworksFromAuthor = FilterForAuthor(author, artworks);
        return ArtworksFromAuthor.OrderBy(artw => artw.month).ThenBy(artw => artw.year).ToArray();
    }

    public static Artwork[] FilterForYear(int year, Artwork[] artworks)
    {
        List<Artwork> artworklist = new List<Artwork>();
        foreach (Artwork artwork in artworks)
        {
            if (artwork.year == year) artworklist.Add(artwork);
        }
        return artworklist.ToArray();
    }

#nullable enable
    public static Artwork? GetArtworkByName(string name, Artwork[] artworks)
    {
        foreach (Artwork artwork in artworks)
        {
            if (artwork.name == name) return artwork;
        }
        return null;
    }
#nullable disable

}
