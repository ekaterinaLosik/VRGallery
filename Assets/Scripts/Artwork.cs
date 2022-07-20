using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @class Artwork
*
* @brief Artwork scriptable object
*
* It's a class that contains all the information about an artwork */
[CreateAssetMenu(fileName = "New Artwork", menuName = "Artwork")]
public class Artwork : ScriptableObject
{
    [Header("Image")]
    public Sprite image;
    [Header("Describing Content")]
    public new string name;
    public string author;
    [TextArea(3, 10)]
    public string description;
    [Header("Estimated Date"), Range(1, 12)]
    public int month;
    public int year;
    [Space(10), Tooltip("Please add the author as tag as well.")]
    public string[] tags;
     [Header("Audio clip")]
    public AudioClip audio;
}