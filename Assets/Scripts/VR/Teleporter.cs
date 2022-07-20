using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;


    /**
    * @class Teleporter
    *
    * @brief Custom teleporter
    *
    * Teleporter, that activates ray only after pressing trigger button */
    public class Teleporter : MonoBehaviour
    {
        public GameObject TeleportRay;
        public TeleportationArea TPArea;
        [SerializeField] private InputActionReference teleportInput;
        [SerializeField] private TeleportationProvider teleportationProvider;

        /// The function is called when the scene starts
        private void Start(){   
            TPArea.teleportationProvider = teleportationProvider;
            if (TeleportRay != null)
                TeleportRay.SetActive(false);
        }

        /// When the script is enabled, enable the teleportInput action, and add the ButtonPressed and
        /// ButtonReleased functions to the started and canceled events.
        private void OnEnable()
        {
            teleportInput.action.Enable();
            teleportInput.action.started += ButtonPressed;
            teleportInput.action.canceled += ButtonReleased;  
        }

        /// When the script is disabled, disable the teleportInput action and remove the event listeners
        /// for the button pressed and button released events
        private void OnDisable()
        {
            teleportInput.action.Disable();
            teleportInput.action.started -= ButtonPressed;
            teleportInput.action.canceled -= ButtonReleased;
        }

        /// When the button is pressed, the TeleportRay is set to active
        /// 
        /// @param obj The context of the action.
        private void ButtonPressed(InputAction.CallbackContext obj)
        {
            TeleportRay.SetActive(true);
        }

       
        /// When the button is released, the raycast is turned off
        /// 
        /// @param obj The context of the action.
        private async void ButtonReleased(InputAction.CallbackContext obj)
        {
            await Task.Delay(100);
            TeleportRay.SetActive(false);
        }

    }

