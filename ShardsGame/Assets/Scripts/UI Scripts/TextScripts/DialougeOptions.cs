using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialougeOptions : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI[] buttonTexts;
    [SerializeField] private SpeachBubbleUI speechBubbleScript;
    private Page[] pages;


    /// <summary>
    ///  will set the dialouge options according to the page: (pages) array that is passed
    /// </summary>
    /// <param name="page"></param>
    public void SetDialougeOptions(DialoguePage page)
    {
        pages = page.Pages;
        SetButtons(page);
        SetActive(true);
        speechBubbleScript.NextButtonInteractable(false);
    }

    /// <summary>
    ///  will set the buttons text according to the page options
    /// </summary>
    /// <param name="page"></param>
    private void SetButtons(DialoguePage page)
    {
        int i = 0;
        foreach (var buttonText in buttonTexts)
        {
            buttonText.text = page.Options[i];
            i++;
        }
    }


    /// <summary>
    ///  will set the options Buttons interactable according to the state
    /// </summary>
    /// <param name="activeState"></param>
    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    /// <summary>
    /// a method to use the option that is clicked
    /// </summary>
    /// <param name="optionIndex"></param>
    public void UseOption(int optionIndex)
    {
        Sheet sheet = new Sheet(1);
        sheet.pages = new Page[1];
        sheet.pages[0] = pages[optionIndex];
        speechBubbleScript.SetSheetUI(sheet);
        SetActive(false);
    }
}