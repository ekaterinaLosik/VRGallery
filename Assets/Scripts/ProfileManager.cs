using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager pManager;
    public GameObject profilePanel;
    public GameObject tagText1;
    public GameObject tagText2;
    private bool _visible;

    private TextMeshProUGUI _text1Comp;
    private TextMeshProUGUI _text2Comp;

    private GameObject _profileButton;
    
    public int[] tagsFrequency = { 0,0,0,0,0 };
    public string[] tags = { "VanGogh", "Gothic", "Modern", "DaVinci", "Klee" };
    
    private void Start()
    {
        pManager = this;
        _visible = false;
        _text1Comp = tagText1.GetComponent<TextMeshProUGUI>();
        _text2Comp = tagText2.GetComponent<TextMeshProUGUI>();
        _profileButton = GameObject.Find("ProfileButton");
    }

    private void Update()
    {
        DetectFavTags();
    }

    public void Sort(int[] arr)
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
    
    private void DetectFavTags()
    {
        Sort(tagsFrequency);
        _text1Comp.text = tags[0];
        _text2Comp.text = tags[1];
    }
    
    public void ShowProfile()
    {
        if (!_visible)
        {
            profilePanel.SetActive(true);       
            _profileButton.SetActive(false);
            _visible = true;
        }
    }

    public void HideProfile()
    {
        if (_visible)
        {
            profilePanel.SetActive(false);
            _profileButton.SetActive(true);
            _visible = false;
        }
    }

    public void UseSpeechToText()
    {
        //TODO invoke Speech-To-Text
    }
    
    public void SavePrefs()
    {
        PlayerPrefs.SetInt(tags[0], tagsFrequency[0]);
        PlayerPrefs.SetInt(tags[1], tagsFrequency[1]);
        PlayerPrefs.SetInt(tags[2], tagsFrequency[2]);
        PlayerPrefs.SetInt(tags[3], tagsFrequency[3]);
        PlayerPrefs.SetInt(tags[4], tagsFrequency[4]);
        PlayerPrefs.Save();
    }
     
    public void LoadPrefs()
    {
        tagsFrequency[0] = PlayerPrefs.GetInt("VanGogh");
        Debug.Log(tagsFrequency[0]);
        tagsFrequency[1] = PlayerPrefs.GetInt("Gothic");
        tagsFrequency[2] = PlayerPrefs.GetInt("Modern");
        tagsFrequency[3] = PlayerPrefs.GetInt("DaVinci");
        tagsFrequency[4] = PlayerPrefs.GetInt("Klee");
    }
}
