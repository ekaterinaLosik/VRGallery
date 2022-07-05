using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using TMPro;


public class KeywordsSpeechRecognition : MonoBehaviour
{
    [Tooltip("Only lowercase")] public string[] taglist;
    public string firstGuess;
    public List<string> secondGuess;
    private DictationRecognizer dr;
    public GameObject FilterManager;
    public Toggle MicroToggle;
    public TextMeshProUGUI ToggleText;

    void Start(){
        MicroToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(MicroToggle);});
    }

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

    public void StartSpeechToText()
    {
        secondGuess.Clear();
        firstGuess = "";
        dr.Start();
    }

    public void StopSpeechToText()
    {
        if (firstGuess == "") firstGuess = "Unklar";
        secondGuess.Remove(firstGuess);
        dr.Stop();        
        StartCoroutine(WaitAndReturnText());

    }

    IEnumerator WaitAndReturnText(){
         yield return new WaitForSeconds(1);
         FilterManager.GetComponent<KeywordManager>().AddToFilterList(ReturnGuesses()[0]);
    }
    public string[] ReturnGuesses()
    {
        // Returns an array with all found tags where the first tag is the one that is most likely what the person said
        string[] retarr = new string[secondGuess.Count + 1];
        retarr[0] = firstGuess;
        secondGuess.CopyTo(retarr, 1);        
        return retarr;
    }

    private void OnEnable()
    {
        secondGuess = new List<string>();
        dr = new DictationRecognizer();
        dr.DictationResult += DictationRecognizer_DictationResult;
        dr.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dr.DictationError += DictationRecognizer_DictationError;
    }

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

    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        Debug.LogWarning(hresult.ToString() + " " + error);
    }

    private void OnDisable()
    {
        if (dr.Status == SpeechSystemStatus.Running) dr.Stop(); 
        dr.DictationResult -= DictationRecognizer_DictationResult;
        dr.DictationError -= DictationRecognizer_DictationError;
        dr.Dispose();
    }

}
