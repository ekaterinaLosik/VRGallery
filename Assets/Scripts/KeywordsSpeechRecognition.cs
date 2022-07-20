using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using TMPro;

/**
* @class KeywordsSpeechRecognition
*
* @brief Speech recognition class
*
* It listens to what you say and tries to find a match in the taglist. If it finds a match it adds it
to the firstGuess or secondGuess list */

public class KeywordsSpeechRecognition : MonoBehaviour
{
    [Tooltip("Only lowercase")] public string[] taglist;
    public string firstGuess;
    public List<string> secondGuess;
    private DictationRecognizer dr;
    public GameObject FilterManager;
    public Toggle MicroToggle;
    public TextMeshProUGUI ToggleText;

    /// It adds a listener to the MicroToggle object.
    void Start(){
        MicroToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(MicroToggle);});
    }

    /// If the toggle is on, start the speech to text function and change the text to "Ein Tag
    /// aussprechen". If the toggle is off, stop the speech to text function and change the text to "zum
    /// Sprechen klicken"
    /// 
    /// @param Toggle The toggle that was changed.
     void ToggleValueChanged(Toggle change)
    {
       if (MicroToggle.isOn) {
        StartSpeechToText();
        ToggleText.text = "Ein Tag aussprechen";
       }
       else {
        StopSpeechToText();
        ToggleText.text = "zum Sprechen klicken";
       }
    }

   /// It starts the speech to text function.
    public void StartSpeechToText()
    {
        secondGuess.Clear();
        firstGuess = "";
        dr.Start();
    }

   /// It stops the speech recognition, and then calls a coroutine that waits for a
   /// second before returning the text
    public void StopSpeechToText()
    {
        if (firstGuess == "") firstGuess = "Unklar";
        secondGuess.Remove(firstGuess);
        dr.Stop();        
        StartCoroutine(WaitAndReturnText());
    }

    /// It waits for 1 second, then it adds the first guess to the filter list
    IEnumerator WaitAndReturnText(){
         yield return new WaitForSeconds(1);
         FilterManager.GetComponent<KeywordManager>().AddToFilterList(ReturnGuesses()[0]);
    }
   
   /// It returns an array of strings where the first string is the most likely tag and the rest of the
   /// strings are the other possible tags
   /// 
   /// @return An array of strings.
    public string[] ReturnGuesses()
    {
        string[] retarr = new string[secondGuess.Count + 1];
        retarr[0] = firstGuess;
        secondGuess.CopyTo(retarr, 1);        
        return retarr;
    }
    
    /// The OnEnable function is called when the script is enabled. It creates a new list of strings
    /// called secondGuess, creates a new DictationRecognizer called dr, and adds the
    /// DictationRecognizer_DictationResult, DictationRecognizer_DictationHypothesis, and
    /// DictationRecognizer_DictationError functions to the dr.DictationResult, dr.DictationHypothesis,
    /// and dr.DictationError events.
    private void OnEnable()
    {
        secondGuess = new List<string>();
        dr = new DictationRecognizer();
        dr.DictationResult += DictationRecognizer_DictationResult;
        dr.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dr.DictationError += DictationRecognizer_DictationError;
    }

    /// The function takes the text that the user is currently saying and splits it into a list of
    /// words. It then adds the entire text to the list. It then checks if any of the words in the list
    /// are in the list of tags. If they are, it adds them to a list of second guesses
    /// 
    /// @param text The text that was recognized by the dictation recognizer.
    private void DictationRecognizer_DictationHypothesis(string text)
    {
        List<string> alltextpossibilities = text.Split(" ").ToList();
        alltextpossibilities.Add(text);
        foreach (string itext in alltextpossibilities)
        {
            if (Array.IndexOf(taglist, itext.ToLower()) > -1)
            {
                if (!secondGuess.Contains(itext.ToLower()))
                {
                    secondGuess.Add(itext.ToLower());
                }
            }
        }
    }

    /// If the user says a word that is in the taglist, then add it to the firstGuess variable. If the
    /// firstGuess variable is not empty, then add the firstGuess variable to the secondGuess list.
    /// Then, set the firstGuess variable to the word that the user just said
    /// 
    /// @param text The text that was recognized.
    /// @param ConfidenceLevel The confidence level of the dictation recognizer.
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        if (Array.IndexOf(taglist, text.ToLower()) > -1)
        {
            if (firstGuess == "")
            {
                firstGuess = text.ToLower();
            } else
            {
                if (!secondGuess.Contains(firstGuess))
                {
                    secondGuess.Add(firstGuess.ToLower());
                }
                firstGuess = text.ToLower();
            }
        }
    }

   /// The function is called when the dictation recognizer encounters an error
   /// 
   /// @param error The error message.
   /// @param hresult The error code.
    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        Debug.LogWarning(hresult.ToString() + " " + error);
    }

    /// The OnDisable function is called when the script is disabled. 
    /// 
    /// **dr.Status == SpeechSystemStatus.Running** checks if the speech recognizer is running. 
    /// 
    /// **dr.Stop()** stops the speech recognizer. 
    /// 
    /// **dr.DictationResult -= DictationRecognizer_DictationResult** removes the event handler for the
    /// DictationResult event. 
    /// 
    /// **dr.DictationError -= DictationRecognizer_DictationError** removes the event handler for the
    /// DictationError event. 
    /// 
    /// **dr.Dispose()** disposes of the speech recognizer.
    private void OnDisable()
    {
        if (dr.Status == SpeechSystemStatus.Running) dr.Stop(); 
        dr.DictationResult -= DictationRecognizer_DictationResult;
        dr.DictationError -= DictationRecognizer_DictationError;
        dr.Dispose();
    }

}
