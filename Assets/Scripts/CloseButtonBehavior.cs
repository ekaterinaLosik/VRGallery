using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* @class CloseButtonBehavior
*
* @brief Behavior for close button
*
* It's a button that closes the game or the menu, depending on the state of the game */
public class CloseButtonBehavior : MonoBehaviour
{
    public GameObject MenuCanvas;
    
    /// The Start() function is called when the script is first run. It adds a listener to the button
    /// that calls the TaskOnClick() function when the button is clicked
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

   /// If the gallery is rendered, close the menu and return to the gallery. If not, close the game
    void TaskOnClick(){
       if (AppState.IsGalleryRendered) MenuCanvas.SetActive(false); //if gallery is rendered, close menu and return to it
       else { 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();}                               //if not, close the game
    }
}
