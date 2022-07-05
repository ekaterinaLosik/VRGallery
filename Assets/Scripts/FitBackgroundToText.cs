using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fit size of object to it's child size
public class FitBackgroundToText : MonoBehaviour
{
    public RectTransform textRect;
    private float padding = 10;
    void Start()
    {
        var rectHeight = this.GetComponent<RectTransform>();
        rectHeight.sizeDelta = new Vector2(0, textRect.rect.height + padding);
    }

}
