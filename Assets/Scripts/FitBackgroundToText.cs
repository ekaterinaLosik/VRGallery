using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @class FitBackgroundToText
*
* @brief Fits background to text
*
* It takes the height of the text and adds a padding to it, then sets the height of the background to
the new height. */
public class FitBackgroundToText : MonoBehaviour
{
    public RectTransform textRect;
    private float padding = 10;

    /// When the object is enabled, get the height of the text and add the padding to it, then set the
    /// height of the object to that value.
    void OnEnable()
    {
        var rectHeight = this.GetComponent<RectTransform>();
        rectHeight.sizeDelta = new Vector2(0, textRect.rect.height + padding);
    }

}
