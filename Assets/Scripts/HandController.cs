using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
/**
* @class HandController
*
* @brief Hand Controller
*
* This class is responsible for reading the values of the grip and trigger actions and passing them to
* the hand class */
public class HandController : MonoBehaviour
{
    ActionBasedController actionBasedController;
    public Hand hand;

    /// It gets the ActionBasedController
    /// component from the game object that this script is attached to
    void Start()
    {
        actionBasedController = GetComponent<ActionBasedController>();
    }

   /// The function is called every frame and it reads the value of the grip and trigger actions and
   /// passes them to the hand object
    void Update()
    {
        hand.SetGrip(actionBasedController.selectAction.action.ReadValue<float>());
        hand.SetTrigger(actionBasedController.activateAction.action.ReadValue<float>());
    }
}
