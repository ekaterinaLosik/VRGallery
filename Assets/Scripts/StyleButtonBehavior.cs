using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleButtonBehavior : MonoBehaviour
{
    public GameObject SpawnManager;
    private GameObject[] oldGalleryPrefabs;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
       

    }

    void TaskOnClick(){
        AppState.IsGalleryRendered = true;
        DestroyGallery();
        SpawnManager.GetComponent<GallerySpawner>().SpawnGallery(name);


    }

    private void DestroyGallery(){
         oldGalleryPrefabs = GameObject.FindGameObjectsWithTag("gallery");
        foreach (GameObject prefab in oldGalleryPrefabs){
            Destroy(prefab);
        }
    }
}
