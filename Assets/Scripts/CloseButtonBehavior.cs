using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonBehavior : MonoBehaviour
{
    public GameObject MenuCanvas;
    
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
       if (AppState.IsGalleryRendered) MenuCanvas.SetActive(false); //if gallery is rendered, close menu and return to it
       else { 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();}                               //if not, close the game
    }
}
