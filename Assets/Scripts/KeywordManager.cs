using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

/**
* @class KeywordManager
*
* @brief Manager for keywords and filters
*
* It's a class that manages the keywords and filters for the artworks */
public class KeywordManager : MonoBehaviour
{
    [SerializeField, Tooltip("Alle Artworks die wir haben hier reinpacken.")] Artwork[] AllArtworks;
    public string[] AllTags;
    public string[] AllAuthors;
    public Artwork[] ArtworkPool;
    public TextMeshProUGUI ArtworkCountField;
    public TextMeshProUGUI CurrentFilter;
    public TextMeshProUGUI CurrentAuthor;
    public Transform FilterButtonsPanel;
    public Transform AuthorButtonsPanel;
    public Toggle FilterButon;
    public Button CVButon;
    public GameObject AlarmPanel;
    public GameObject ProfileManager;
    private List<string> tagsList;

    /// The function is called when the application starts. It resets the pool of artworks, creates a
    /// list of tags, gets all the tags from the artworks, gets all the authors from the artworks,
    /// creates the filter buttons, and creates the author buttons
    private void Start()
    {
        ResetPool();
        tagsList = new List<string>();
        AllTags = Filters.GetAllTags(AllArtworks);
        AppState.AllTags = AllTags;
        AllAuthors = Filters.GetAllAuthors(AllArtworks);
        gameObject.GetComponent<KeywordsSpeechRecognition>().taglist = AllTags;
        CountArtworks();
        CreateFilterButtons();
        CreateAuthorButtons();
    }

    /// If the artwork is found, it is added to the ArtworkPool. If it is not found, the ArtworkPool is
    /// emptied
    /// 
    /// @param name The name of the artwork to search for
    public void GetArtworkByName(string name)
    {
#nullable enable
        Artwork? found = Filters.GetArtworkByName(name, AllArtworks);
        if (found == null) ArtworkPool = new Artwork[] { };
        else ArtworkPool = new Artwork[] { found };

#nullable disable
    }

    /// It takes a year as an argument, and then filters the ArtworkPool to only contain artworks from
    /// that year
    /// 
    /// @param year The year to filter for
    public void FilterForYear(int year)
    {
        ArtworkPool = Filters.FilterForYear(year, ArtworkPool);
    }

    /// It takes a string as an argument, filters the list of artworks for the given author and then
    /// sets the text of the current author text field to the given author
    /// 
    /// @param author The name of the author to filter for
    public void FilterForCv(string author)
    {
        ArtworkPool = Filters.FilterForCv(author, AllArtworks);
        CurrentAuthor.text = "Aktueller Autor: " + author;
    }

    /// If there's only one tag, then filter the artworks for that tag. If there's more than one tag,
    /// then filter the artworks for each tag and add them to a list. If there's no tags, then set the
    /// artwork pool to null
    /// 
    /// @param tags A list of tags to filter for.
    public void FilterForTag(List<string> tags)
    {
        if(tags.Count == 1) { 
            ArtworkPool = Filters.FilterForTag(tags.First(), AllArtworks); 
        } else if(tags.Count > 1)
        {
            List<Artwork> artworkslist = new List<Artwork>();
            foreach(string itag in tags)
            {
                artworkslist.AddRange(Filters.FilterForTag(itag, AllArtworks));
            }
            ArtworkPool = artworkslist.Distinct().ToArray();
        } else
        {
            ArtworkPool = null;
        }
        CountArtworks();   
    }

  
    /// It counts the number of artworks in the ArtworkPool and displays it in the ArtworkCountField
    public void CountArtworks(){
         ArtworkCountField.text = "Anzahl der Bilder: " + ArtworkPool.Count();
    }

    /// If the tagsList is empty, return true, else return false
    /// 
    /// @return The method returns a boolean value.
    private bool IsTagsListEmpty(){
        if (tagsList.Count() == 0)
        return true;
        else return false;
    }
    
    /// It creates a toggle button for each tag in the AllTags list, and adds a listener to each button
    /// that adds the button's text to the filter list when the button is toggled on.
    public void CreateFilterButtons(){
        foreach (string tag in AllTags) {
            Toggle button;
            button = Instantiate(FilterButon, FilterButtonsPanel);
            TextMeshProUGUI buttonLabel = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonLabel.text = tag;
            button.onValueChanged.AddListener(delegate {
            AddToFilterList(buttonLabel.text);});

        }
    }

   /// For each author in the list of all authors, create a button, set the button's label to the
   /// author's name, and add a listener to the button that calls the function FilterForCv() when the
   /// button is clicked
    public void CreateAuthorButtons(){
        foreach (string author in AllAuthors){
            Button button;
            button = Instantiate(CVButon, AuthorButtonsPanel);
            TextMeshProUGUI buttonLabel = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonLabel.text = author;
            button.onClick.AddListener(delegate {
            FilterForCv(buttonLabel.text);});
        }
    }

    /// If the filter is in the list of all tags, then add it to the list of filters. If it's not in the
    /// list of all tags, then display an alarm panel
    /// 
    /// @param filter the string that is being added to the list
    public void AddToFilterList(string filter){
        if (AllTags.Contains(filter)){
        if(tagsList.Contains(filter)) {
            tagsList.Remove(filter);
        }
        
        else {
            tagsList.Add(filter);
        }
        DisplayFilters();
        }
        else{
            AlarmPanel.SetActive(true);
        }
    }
    
    /// If the list of tags is empty, display all artworks. If the list of tags is not empty, filter the
    /// artworks for the tags in the list.
    private void DisplayFilters(){
        if(IsTagsListEmpty()){
            CurrentFilter.text = "Filter auswählen: ";
            ArtworkPool = AllArtworks;            
            CountArtworks();

            }
        else{
            Debug.Log(ArtworkPool.Count());
            FilterForTag(tagsList);
             CurrentFilter.text = "Aktuelle Filter: " + String.Join(", ", tagsList.ToArray());
            }
    }

     /// It saves the current artworks list to the AppState.CurrentArtworkslist variable
     public void SaveActualArtworkpool(){
        AppState.CurrentArtworkslist = ArtworkPool;
        if (!IsTagsListEmpty())
        ProfileManager.GetComponent<ProfileManager>().GetLastTags(tagsList);
    }

    /// It resets the pool of artworks to the full list of artworks, resets the filter and author text
    /// and counts the artworks
    public void ResetPool(){
        ArtworkPool = AllArtworks;
        CurrentFilter.text = "Filter auswählen: ";
        CurrentAuthor.text = "Author auswählen: ";
        CountArtworks();
    }
}
