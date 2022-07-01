using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour
{
    public int totalTutorialSites;
    public int currentSite;
    public GameObject firstPanel;
    public GameObject secondPanel;

    private GameObject[] _panels;
        
    void Start()
    {
        currentSite = 0;
        totalTutorialSites = 2;
        _panels = new[] { firstPanel, secondPanel };
    }

    public void NextSite()
    {
        _panels[currentSite].SetActive(false);
        currentSite++;
        _panels[currentSite].SetActive(true);
    }

    public void PreviousSite()
    {
        _panels[currentSite].SetActive(false);
        currentSite--;
        _panels[currentSite].SetActive(true);
    }
}
