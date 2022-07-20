using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @class VRResetPlayerPosition
*
* @brief Reset player position
*
* It resets the player's position to the position of the reset transform */
public class VRResetPlayerPosition : MonoBehaviour
{
    public Transform ResetTransform;
    public GameObject Player;
    public Camera Camera;

    /// The function resets the player's position and rotation
    public void ResetPosition(){
        var rotationY = ResetTransform.rotation.eulerAngles.y - Camera.transform.rotation.eulerAngles.y;
        Player.transform.Rotate(0, rotationY, 0);
        var positionDelta = ResetTransform.position - Camera.transform.position;
        Player.transform.position += positionDelta;
    }
}
