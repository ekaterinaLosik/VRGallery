using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

/**
* @class GallerySpawner
*
* @brief Spawns a gallery
*
* It spawns a gallery of paintings, each of which has an image, a name, an author, a year, a
description and an audio clip. */
public class GallerySpawner : MonoBehaviour
{
    //for Prefabs
    public GameObject WallWithoutPaintingClassic;
    public GameObject WallWithoutPaintingWood;
    public GameObject WallWithoutPaintingBricks;
    public GameObject classic;
    public GameObject wood;
    public GameObject bricks;
    public Transform TPArea;
    //dimension of prefab models
    public float size = 4;

    private GameObject prefab;
    private GameObject wallWithoutPainting;

    private Artwork[] ArtworkPool;
    private int artworkCount;
    private Artwork artwork;
    private GameObject currentPrefab;
    private Image image;
    private TextMeshProUGUI infoText;
    private GuideVoice guidevoice;
    private int index; 

    /// It instantiates a prefab with a painting on it
    void Spawn()
    {
       
        Vector3 entrancePosition = new Vector3(0,size/2,-size/2);
      
        Vector3 endingPosition = new Vector3(0, size/2, size*artworkCount - size/2);

        Instantiate (wallWithoutPainting, entrancePosition, Quaternion.identity);
        Instantiate (wallWithoutPainting, endingPosition, Quaternion.identity);
        Vector3 leftPrefabPosition = new Vector3(0,0,0);
        Vector3 rightPrefabPosition = new Vector3(0,0,size);
        Quaternion rightPrefabRotation = Quaternion.Euler(0, 180, 0);
        Vector3 tpScale = new Vector3(0.3f, 0.1f, 0.1f * artworkCount*size);
        Vector3 tpPosition = new Vector3(0,0,artworkCount*size/2 - size/2);
        TPArea.position = tpPosition;
        TPArea.localScale = tpScale;
        index = 0;
        while (artworkCount > 0){  
            artwork = ArtworkPool[index];
            currentPrefab = Instantiate(prefab, leftPrefabPosition, Quaternion.identity);
            SetArtwork();
            if (artworkCount > 0){
                artwork = ArtworkPool[index];
                currentPrefab = Instantiate(prefab, rightPrefabPosition, rightPrefabRotation);
                SetArtwork();
            }
            leftPrefabPosition.z += size*2;
            rightPrefabPosition.z += size*2;
        }
    }

   /// It sets the image, text, and audio of the artwork 
    private void SetArtwork(){
        image = currentPrefab.GetComponentInChildren(typeof(Image)) as Image;
        infoText = currentPrefab.GetComponentInChildren(typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
        image.sprite = artwork.image;    
        infoText.text = System.Environment.NewLine + artwork.name + ", " + artwork.author + ", " + artwork.year + System.Environment.NewLine + System.Environment.NewLine + artwork.description + System.Environment.NewLine;
        guidevoice = currentPrefab.GetComponentInChildren(typeof(GuideVoice), true) as GuideVoice;
        guidevoice.audioClip = artwork.audio;
        artworkCount --;
        index++;
    }

   
    
    /// It counts the number of artworks
    public void CountGalleryLength(){
        ArtworkPool = AppState.CurrentArtworkslist;
        artworkCount = ArtworkPool.Length;
        Debug.Log(artworkCount);
    }

    /// It takes a string as an argument, sets the style of the gallery, counts the
    /// length of the gallery, and then spawns the gallery.
    /// 
    /// @param style The style of the gallery.
    public void SpawnGallery(string style){
        SetStyle(style);
        CountGalleryLength();
        Spawn();
    }
       

    /// This function is called when the user selects a style from the style menu. It sets the prefab
    /// variable to the prefab of the selected style and sets the wallWithoutPainting variable to the
    /// wallWithoutPainting prefab of the selected style
    /// 
    /// @param style the style of the wall
    public void SetStyle(string style){
        if (style.Equals("classic")){
            prefab = classic;
            wallWithoutPainting = WallWithoutPaintingClassic;
        }
        else if (style.Equals("wood")){
            prefab = wood;
            wallWithoutPainting = WallWithoutPaintingWood;
        }
        else if(style.Equals("bricks")){
            prefab = bricks;
            wallWithoutPainting = WallWithoutPaintingBricks;
        }
        ProfileManager.pManager.IncreaseStyleFrequency(style);
    }

}
