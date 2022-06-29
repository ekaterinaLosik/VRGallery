using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideVoice : MonoBehaviour
{

    public ActivationMode activationMode = ActivationMode.ButtonTriggerd;
    public AudioClip audioClip;

    private AudioSource audioSource;

    private Sprite speakerOnSprite;
    private Sprite speakerOffSprite;

    private bool isSpeaking;

    public enum ActivationMode
    {
        ButtonTriggerd,
        ProximityTriggerd
    }

    private void Start()
    {
        speakerOnSprite = Resources.Load<Sprite>("Sprites/SpeakerIcon");
        speakerOffSprite = Resources.Load<Sprite>("Sprites/SpeakerOffIcon");

        if (activationMode == ActivationMode.ProximityTriggerd)
            GetComponentInChildren<Canvas>().enabled = false;

        audioSource = GetComponent<AudioSource>();

        if (audioSource.clip == null)
            audioSource.clip = audioClip;
        
        if (audioSource == null || audioSource.clip == null)
            Debug.LogError("Critical: Audiosource or Clip is null");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
            audioSource.Play();

    }

    private void OnTriggerExit(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
            audioSource.Stop();
    }

    public void ToggleVoice()
    {
        if (isSpeaking)
        {
            audioSource.Stop();
            isSpeaking = false;
            GetComponentInChildren<Button>().image.sprite = speakerOffSprite;
        }
        else
        {
            audioSource.Play();
            isSpeaking = true;
            GetComponentInChildren<Button>().image.sprite = speakerOnSprite;
        }
    }
}
