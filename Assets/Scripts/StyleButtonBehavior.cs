using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
* @class StyleButtonBehavior
*
* @brief Behavior for style button
*
* When button clicked, destroys the current gallery and spawns a new one */
public class StyleButtonBehavior : MonoBehaviour
{
    public GameObject SpawnManager;
    private GameObject[] oldGalleryPrefabs;

   /// The function is called when the script is first run. It adds a listener to the button
   /// that calls the TaskOnClick() function when the button is clicked
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    /// When the user clicks on a task, the old galley is destroyed, the new gallery is spawned
    void TaskOnClick(){
        AppState.IsGalleryRendered = true;
        DestroyGallery();
        SpawnManager.GetComponent<GallerySpawner>().SpawnGallery(name);
    }

    /// It finds all the objects with the tag "gallery" and destroys them.
    private void DestroyGallery(){
         oldGalleryPrefabs = GameObject.FindGameObjectsWithTag("gallery");
        foreach (GameObject prefab in oldGalleryPrefabs){
            Destroy(prefab);
        }
    }
}
