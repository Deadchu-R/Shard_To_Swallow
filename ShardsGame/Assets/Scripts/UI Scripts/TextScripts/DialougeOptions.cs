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


    public void SetDialougeOptions(DialoguePage page)
    {
        pages = page.Pages;
        SetButtons(page);
        SetActive(true);
        speechBubbleScript.NextButtonInteractable(false);
    }

    private void SetButtons(DialoguePage page)
    {
        int i = 0;
        foreach (var buttonText in buttonTexts)
        {
            buttonText.text = page.Options[i];
            i++;
        }
    }


    public void SetActive(bool activeState)
    {
        gameObject.SetActive(activeState);
    }

    public void UseOption(int optionIndex)
    {
        //Page[] newPages = new Page[1];
        Sheet sheet = new Sheet(1);
        sheet.pages = new Page[1];
        sheet.pages[0] = pages[optionIndex];
        speechBubbleScript.SetSheetUI(sheet);
        SetActive(false);
    }
}