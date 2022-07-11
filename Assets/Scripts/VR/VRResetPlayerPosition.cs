using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRResetPlayerPosition : MonoBehaviour
{
    public Transform ResetTransform;
    public GameObject Player;
    public Camera Camera;

    public void ResetPosition(){
        var rotationY = ResetTransform.rotation.eulerAngles.y - Camera.transform.rotation.eulerAngles.y;
        Player.transform.Rotate(0, rotationY, 0);
        var positionDelta = ResetTransform.position - Camera.transform.position;
        Player.transform.position += positionDelta;
    }
}
