using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

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
        int index = 0;
        while (artworkCount > 0){  
            Artwork artwork = ArtworkPool[index];
            
            GameObject currentPrefab = Instantiate(prefab, leftPrefabPosition, Quaternion.identity);
            Image image = currentPrefab.GetComponentInChildren(typeof(Image)) as Image;
            TextMeshProUGUI infoText = currentPrefab.GetComponentInChildren(typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            image.sprite = artwork.image;
            infoText.text = artwork.name + ", " + artwork.author + ", " + artwork.year + System.Environment.NewLine + artwork.description;
            GuideVoice guidevoice = currentPrefab.GetComponentInChildren(typeof(GuideVoice), true) as GuideVoice;
            guidevoice.audioClip = artwork.audio;
            artworkCount --;
            index++;

            if (artworkCount > 0){
                artwork = ArtworkPool[index];
                currentPrefab = Instantiate(prefab, rightPrefabPosition, rightPrefabRotation);
                image = currentPrefab.GetComponentInChildren(typeof(Image)) as Image;
                image.sprite = artwork.image;
                infoText = currentPrefab.GetComponentInChildren(typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
                infoText.text = artwork.name + ", " + artwork.author + ", " + artwork.year + System.Environment.NewLine + artwork.description;
                guidevoice = currentPrefab.GetComponentInChildren(typeof(GuideVoice), true) as GuideVoice;
                guidevoice.audioClip = artwork.audio;
                artworkCount --;
                index++;
            }
            leftPrefabPosition.z += size*2;
            rightPrefabPosition.z += size*2;
        }
    }

   
    
    public void CountGalleryLength(){
        ArtworkPool = AppState.CurrentArtworkslist;
        artworkCount = ArtworkPool.Length;
        Debug.Log(artworkCount);
    }

    public void SpawnGallery(string style){
        SetStyle(style);
        CountGalleryLength();
        Spawn();
    }
       

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
    }

}
