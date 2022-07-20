using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @class AppState
*
* @brief Static app state class
*
* This class is a static class that holds all the data that is used throughout the application */
public static class AppState
{
   public static bool IsGalleryRendered = false;
   public static Artwork[] CurrentArtworkslist;
   public static List<string> FavoriteFilters;
   public static Artwork[] AllArtworks;
   public static string[] AllTags;
}
