using System;
using System.Collections.Generic;
using System.Linq;

/**
* @class Filters
*
* @brief Filter artwors
*
* It's a class that contains methods that filter an array of Artwork objects based on different criteria */
public static class Filters
{
    public static List<Artwork> SelectedArtworkslist;

   /// It takes an array of Artwork objects, loops through each Artwork object, loops through each tag
   /// in the Artwork object, and adds the tag to a list of tags if it doesn't already exist in the list
   /// 
   /// @param artworks The array of artworks to get the tags from.
   /// 
   /// @return An array of strings.
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

   /// It takes an array of Artwork objects, loops through each one, and adds the author to a list if
   /// it's not already in the list
   /// 
   /// @param artworks An array of Artwork objects.
   /// 
   /// @return An array of strings
    public static string[] GetAllAuthors(Artwork[] artworks)
    {
        List<string> artworklist = new List<string>();
        foreach (Artwork artwork in artworks)
        {
           if(!artworklist.Contains(artwork.author)) artworklist.Add(artwork.author);
        }
        return artworklist.ToArray();
    }

    /// It takes an array of Artwork objects, loops through each one, and adds the year to a list if
    /// it's not already in the list
    /// 
    /// @param artworks The array of Artwork objects
    /// 
    /// @return A list of strings
    public static List<string> GetAllYears(Artwork[] artworks)
    {
        List<string> artworklist = new List<string>();
        foreach (Artwork artwork in artworks)
        {
            if(!artworklist.Contains(artwork.year.ToString())) artworklist.Add(artwork.year.ToString());
        }
        return artworklist;
    }


   /// It takes an array of artworks and a tag, and returns an array of artworks that have that tag
   /// 
   /// @param tag The tag to filter for
   /// @param artworks The array of Artwork objects to filter
   /// 
   /// @return An array of Artwork objects.
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

   /// It takes an array of Artwork objects and a string, and returns an array of Artwork objects that
   /// have the same author as the string
   /// 
   /// @param author The author to filter for
   /// @param artworks The array of artworks to filter
   /// 
   /// @return An array of Artwork objects.
    public static Artwork[] FilterForAuthor(string author, Artwork[] artworks)
    {
        List<Artwork> artworklist = new List<Artwork>();
        foreach (Artwork artwork in artworks)
        {
            if (artwork.author.ToLower() == author.ToLower()) artworklist.Add(artwork);
        }
        return artworklist.ToArray();
    }

  /// It takes an author and an array of artworks, filters the array for the author, and then orders the
  /// array by month and year
  /// 
  /// @param author
  /// @param artworks an array of Artwork objects
  /// 
  /// @return An array of Artwork objects.
    public static Artwork[] FilterForCv(string author, Artwork[] artworks)
    {
        Artwork[] ArtworksFromAuthor = FilterForAuthor(author, artworks);
        return ArtworksFromAuthor.OrderBy(artw => artw.month).ThenBy(artw => artw.year).ToArray();
    }

   /// It takes an array of Artwork objects and returns an array of Artwork objects that were created in
   /// the year specified by the year parameter
   /// 
   /// @param year The year to filter for
   /// @param artworks The array of Artwork objects to filter.
   /// 
   /// @return An array of Artwork objects.
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
    /// It takes a string and an array of Artwork objects, and returns the first Artwork object in the
    /// array whose name property matches the string
    /// 
    /// @param name The name of the artwork you want to get.
    /// @param artworks The array of Artwork objects that you want to search through.
    /// 
    /// @return The first artwork with the name that matches the name parameter.
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
