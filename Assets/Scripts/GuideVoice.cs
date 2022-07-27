using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class GuideVoice
 * 
 * @brief handels voice activation for artwork description
 *
 * Plays an audioclip with the artwork description
 */
public class GuideVoice : MonoBehaviour
{
    /// current ActivationMode
    private ActivationMode _activationMode = ActivationMode.ButtonTriggerd;

    /// prepares for new interaction based on choosen ActivationMode
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

    /// is the audioclip currently playing
    private bool _isSpeaking = false;

    /// sets of image sprite based on if the audioclip is playing
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

    /// audioclip to be played
    public AudioClip audioClip;

    /// audiosource to be used
    private AudioSource audioSource;

    /// holds playArrowSprite
    private Sprite playArrowSprite;

    /// holds stopSprite
    private Sprite stopSprite;

    /// ButtonTriggerd can be used when you want the user to press a button to play the audioclip,
    /// ProximityTriggerd is used when you want to play the clip if the user comes near the artwork
    public enum ActivationMode
    {
        ButtonTriggerd,
        ProximityTriggerd
    }

    /// initialize sprites, sets activationMode, sets audioclip
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

    /// updates isSpeaking, based on audioSource.isPlaying, to be processed further
    private void Update()
    {
        if (activationMode == ActivationMode.ButtonTriggerd)
        {
            isSpeaking = audioSource.isPlaying;
        }
    }

    /// starts to play the audioclip if collides with MainCamera
    /// 
    /// @param other objects which this is colliding with
    private void OnTriggerEnter(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
        {
            audioSource.Play();
        }
    }

    /// stops to play the audioclip if collider exiting is MainCamera
    /// 
    /// @param objects which this is colliding with
    private void OnTriggerExit(Collider other)
    {
        if (activationMode == ActivationMode.ProximityTriggerd &&
            other.gameObject.CompareTag("MainCamera"))
        {
            audioSource.Stop();
        }
    }

    /// toogels guide voice
    public void ToggleVoice()
    {
        isSpeaking = !isSpeaking;
    }
}
