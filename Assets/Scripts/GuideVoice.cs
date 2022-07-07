using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideVoice : MonoBehaviour
{
    private ActivationMode _activationMode = ActivationMode.ProximityTriggerd;
    public ActivationMode activationMode
    {
        get
        {
            return _activationMode;
        }
        set
        {
            _activationMode = value;
            GetComponentInChildren<Canvas>().enabled = _activationMode == ActivationMode.ButtonTriggerd;
        }
    }

    private bool _isSpeaking = false;
    private bool isSpeaking
    {
        get
        {
            return _isSpeaking;
        }
        set
        {
            // Value is already set, no need to play/stop audiosource or change sprite
            if (_isSpeaking == value)
            {
                return;
            }

            if (value)
            {
                audioSource.Play();
                GetComponentInChildren<Button>().image.sprite = stopSprite;
            }
            else
            {
                audioSource.Stop();
                GetComponentInChildren<Button>().image.sprite = playArrowSprite;
            }
            _isSpeaking = value;
        }
    }

    public AudioClip audioClip;

    private AudioSource audioSource;

    private Sprite playArrowSprite;
    private Sprite stopSprite;

    public enum ActivationMode
    {
        ButtonTriggerd,
        ProximityTriggerd
    }

    private void Start()
    {
        playArrowSprite = Resources.Load<Sprite>("Sprites/PlayArrow");
        stopSprite = Resources.Load<Sprite>("Sprites/Stop");

        if (activationMode == ActivationMode.ProximityTriggerd)
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource.clip == null)
        {
            audioSource.clip = audioClip;
        }

        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogError("Critical: Audiosource or Clip is null");
        }
    }

    private void Update()
    {
        if (activationMode == ActivationMode.ButtonTriggerd)
        {
            isSpeaking = audioSource.isPlaying;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
        {
            audioSource.Stop();
        }
    }

    public void ToggleVoice()
    {
        isSpeaking = !isSpeaking;
    }
}
