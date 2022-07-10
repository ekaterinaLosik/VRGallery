using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager pManager;
    public GameObject profilePanel;
    public GameObject tagText1;
    public GameObject tagText2;
    public GameObject galleryStyle;
    public GameObject textInput;
    public Sprite style1;
    public Sprite style2;
    public Sprite style3;
    public int[] tagsFrequency = { 0,0,0,0,0 };
    public string[] tags = { "VanGogh", "Gothic", "Modern", "DaVinci", "Klee" };
    public int[] styleFrequency = { 0, 0, 0 };
    public Sprite[] styles;
    public string[] stylesName = { "1","2","3"};
    
    private Image _imageRenderer;
    private GameObject _profileButton;
    private GameObject _closeProfileButton;
    private TextMeshProUGUI _text1Comp;
    private TextMeshProUGUI _text2Comp;
    private TextMeshProUGUI _textInput;
    private bool _visible;
    private Dictionary<string, Sprite> _styleDictionary;
    private string _interests;


    private void Awake()
    {
        pManager = this;
        _visible = false;
        _text1Comp = tagText1.GetComponent<TextMeshProUGUI>();
        _text2Comp = tagText2.GetComponent<TextMeshProUGUI>();
        _textInput = textInput.GetComponent<TextMeshProUGUI>();
        _profileButton = GameObject.Find("ProfileButton");
        _imageRenderer = galleryStyle.GetComponent<Image>();
        _interests = "";
        styles = new []{ style1, style2, style3 };
    }

    private void Start()
    {
        InitDictionary();
        LoadProfileData();
        //CreateTagFrequencyArray();
    }

    private void Update()
    {
        DetectFavTags();
        DetectFavStyle();
    }

    private void InitDictionary()
    {
        _styleDictionary = new Dictionary<string, Sprite>
        {
            { "1", style1 },
            { "2", style2 },
            { "3", style3 }
        };
    }

    public void SortTags(int[] arr)
    {
        for(int i = 1; i<arr.Length; i++) {
            for(int e = 0; e<arr.Length - i; e++) {
                if((arr[e] - arr[e+1]) < 0) {
                    (arr[e], arr[e+1]) = (arr[e+1], arr[e]);
                    (tags[e], tags[e + 1]) = (tags[e + 1], tags[e]);
                }
            }
        }
    }
    
    public void SortStyles(int[] arr)
    {
        for(int i = 1; i<arr.Length; i++) {
            for(int e = 0; e<arr.Length - i; e++) {
                if((arr[e] - arr[e+1]) < 0) {
                    (arr[e], arr[e+1]) = (arr[e+1], arr[e]);
                    (styles[e], styles[e + 1]) = (styles[e + 1], styles[e]);
                    (stylesName[e], stylesName[e + 1]) = (stylesName[e + 1], stylesName[e]);
                }
            }
        }
    }
    
    private void DetectFavTags()
    {
        SortTags(tagsFrequency);
        _text1Comp.text = tags[0];
        _text2Comp.text = tags[1];
    }

    private void DetectFavStyle()
    {
        SortStyles(styleFrequency);
        if (_imageRenderer.sprite != styles[0])
        {
            ChangeSprite(styles[0]);
        }
    }
    
    public void ShowProfile()
    {
        if (!_visible)
        {
            profilePanel.SetActive(true);       
            _profileButton.SetActive(false);
            if (_closeProfileButton == null)
            {
                _closeProfileButton = GameObject.Find("CloseButton");
            }
            _closeProfileButton.SetActive(true);
            _visible = true;
        }
    }

    public void HideProfile()
    {
        if (_visible)
        {
            profilePanel.SetActive(false);
            _profileButton.SetActive(true);
            _closeProfileButton.SetActive(false);
            _visible = false;
            
            SaveProfileData();
        }
    }

    public void UseSpeechToText()
    {
        //TODO invoke SpeechToText
        Debug.Log("Called");
    }

    private int[] CreateTagFrequencyArray()
    {
        //TODO getAllTagsArray[] --> to global class Array
        string[] tempArray = new string[5];
        int[] frequencyArray = new int[tempArray.Length];
        
        for (int i = 0; i < tempArray.Length; i++)
        {
            frequencyArray[i] = 0;
        }

        return frequencyArray;
    }

    private int FindTagIndex(string filter)
    {
        //TODO get global class Array with constantly updated Tags
        string[] tempArray = new string[5];

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i].Equals(filter))
            {
                return i;
            }
        }

        return -1;
    }

    private void UpdateFrequencyArrayForTag(int filterIndex)
    {
        //TODO get global class Array with constantly updated TagFrequency
        int[] tempFreqArray = new int[5];
        tempFreqArray[filterIndex] += 1;
    }
    
    public void UpdateProfileData(string filter)
    {
        //TODO should be called whenever a tag has been chosen
        int idx = FindTagIndex(filter);
        UpdateFrequencyArrayForTag(FindTagIndex(filter));
    }
    

    private void ChangeSprite(Sprite sprite)
    {
        _imageRenderer.sprite = sprite;
    }

    private void SaveProfileData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/profileData.dat", FileMode.Create);
        ProfileDataContainer pdc = new ProfileDataContainer();
        
        pdc.tagsFrequency = tagsFrequency;
        pdc.styleFrequency = styleFrequency;
        pdc.tags = tags;
        pdc.styleName = stylesName;
        pdc.interests = _textInput.text;

        bf.Serialize(file, pdc);
        file.Close();
    }

    private void LoadProfileData()
    {
        if (File.Exists(Application.dataPath + "/profileData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/profileData.dat", FileMode.Open);
            ProfileDataContainer pdc = (ProfileDataContainer)bf.Deserialize(file);
            file.Close();
            
            tagsFrequency = pdc.tagsFrequency;
            tags = pdc.tags;
            styleFrequency = pdc.styleFrequency;
            stylesName = pdc.styleName;
            _interests = pdc.interests;

            ReorderSprites();
            DisplayInterests();
            
        }
    }

    //Method to reorder the Sprite-Array after loading the profile data (gallery style preference)
    private void ReorderSprites()
    {
        for (int i = 0; i < stylesName.Length; i++)
        {
            string styleName = stylesName[i];
            Sprite style = _styleDictionary[styleName];
            styles[i] = style;
        }
    }

    private void DisplayInterests()
    {
        _textInput.text = _interests;
    }
}

[Serializable]
public class ProfileDataContainer
{
    public int[] tagsFrequency;
    public int[] styleFrequency;
    public string[] tags;
    public string[] styleName;
    public string interests;
}
