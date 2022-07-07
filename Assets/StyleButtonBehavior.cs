using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleButtonBehavior : MonoBehaviour
{
    public GameObject SpawnManager;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
       SpawnManager.GetComponent<GallerySpawner>().SpawnGallery(name);

    }
}
