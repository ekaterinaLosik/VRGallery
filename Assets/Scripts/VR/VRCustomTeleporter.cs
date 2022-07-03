using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;


    public class VRCustomTeleporter : MonoBehaviour
    {
        [SerializeField] private InputActionReference teleportInput;
        [SerializeField] private GameObject teleportRay;
        [SerializeField] private TeleportationProvider teleportationProvider;

        private bool wallWasHit;

        private void Start()
        {
            var areas = FindObjectsOfType<TeleportationArea>();
            foreach (var area in areas)
            {
                area.teleportationProvider = teleportationProvider;
            }

            if (teleportRay != null)
                teleportRay.SetActive(false);
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
            teleportRay.SetActive(true);
        }

       

        private async void ButtonReleased(InputAction.CallbackContext obj)
        {
            // safety wait until player has teleported
            await Task.Delay(100);

            // Deactivate ray again
            teleportRay.SetActive(false);
        }

    }

