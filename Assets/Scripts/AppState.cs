using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AppState
{
   public static bool IsGalleryRendered = false;
   public static Artwork[] CurrentArtworkslist;
   public static List<string> FavoriteFilters;
   public static Artwork[] AllArtworks;
   public static string[] AllTags;
}
