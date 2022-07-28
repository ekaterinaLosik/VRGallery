using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @class MenuManagerScript
*
* @brief Controls the tutorial-menu
*
* It provides the tutorial navigation, including next site & previous site */

public class MenuManagerScript : MonoBehaviour
{
    /// indicates how many tutorial sites there are and which one is currently visible
    public int totalTutorialSites;
    public int currentSite;
    /// for the different tutorial sites
    public GameObject firstPanel;
    public GameObject secondPanel;

    private GameObject[] _panels;
        
    /// The function is called when the application starts. It sets the currently visible site and how many tutorial sites
    /// there are. Furthermore it fills an array with the panels for each tutorial site.
    void Start()
    {
        currentSite = 0;
        totalTutorialSites = 2;
        _panels = new[] { firstPanel, secondPanel };
    }

    /// It navigates to the next tutorial site
    public void NextSite()
    {
        _panels[currentSite].SetActive(false);
        currentSite++;
        _panels[currentSite].SetActive(true);
    }

    /// It navigates to the previous tutorial site
    public void PreviousSite()
    {
        _panels[currentSite].SetActive(false);
        currentSite--;
        _panels[currentSite].SetActive(true);
    }
}
