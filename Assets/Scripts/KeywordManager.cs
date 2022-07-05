using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

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

    public void GetArtworkByName(string name)
    {
#nullable enable
        Artwork? found = Filters.GetArtworkByName(name, AllArtworks);
        if (found == null) ArtworkPool = new Artwork[] { };
        else ArtworkPool = new Artwork[] { found };

#nullable disable
    }

    public void FilterForYear(int year)
    {
        ArtworkPool = Filters.FilterForYear(year, ArtworkPool);
    }

    public void FilterForCv(string author)
    {
        ArtworkPool = Filters.FilterForCv(author, ArtworkPool);
        CurrentAuthor.text = "Aktueller Autor: " + author;
    }

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

  
    public void CountArtworks(){
         ArtworkCountField.text = "Anzahl der Bilder: " + ArtworkPool.Count();
    }

    private bool IsTagsListEmpty(){
        if (tagsList.Count() == 0)
        return true;
        else return false;
    }
    
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

     public void SaveActualArtworkpool(){
        AppState.CurrentArtworkslist = ArtworkPool;
        if (!IsTagsListEmpty())
        ProfileManager.GetComponent<ProfileManager>().GetLastTags(tagsList);
    }

    public void ResetPool(){
        ArtworkPool = AllArtworks;
        CurrentFilter.text = "Filter auswählen: ";
        CurrentAuthor.text = "Author auswählen: ";
        CountArtworks();
    }
}
