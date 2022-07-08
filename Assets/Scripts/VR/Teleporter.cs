using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;


    public class Teleporter : MonoBehaviour
    {
        public GameObject TeleportRay;
        public TeleportationArea TPArea;
        [SerializeField] private InputActionReference teleportInput;
        [SerializeField] private TeleportationProvider teleportationProvider;

        private void Start(){   
            TPArea.teleportationProvider = teleportationProvider;
            if (TeleportRay != null)
                TeleportRay.SetActive(false);
        }

        private void OnEnable()
        {
            teleportInput.action.Enable();
            teleportInput.action.started += ButtonPressed;
            teleportInput.action.canceled += ButtonReleased;  
        }

        private void OnDisable()
        {
            teleportInput.action.Disable();
            teleportInput.action.started -= ButtonPressed;
            teleportInput.action.canceled -= ButtonReleased;
        }

        private void ButtonPressed(InputAction.CallbackContext obj)
        {
            TeleportRay.SetActive(true);
        }

       
        private async void ButtonReleased(InputAction.CallbackContext obj)
        {
            await Task.Delay(100);
            TeleportRay.SetActive(false);
        }

    }

