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
        Debug.Log(pages[0].Text);
        
        SetButtons(page);
        gameObject.SetActive(true);
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

    public void UseOption(int optionIndex)
    {
       // this.pages[0] = pages[optionIndex];
         
        //Sheet sheet = new Sheet();
        //sheet.pages = pages;
        // Debug.Log(sheet.pages[optionIndex].Text);
        Page[] newPages = pages; 
       //newPages[]
        speechBubbleScript.SetSheetUI(pages, optionIndex);
        //speechBubbleScript.SetPageUI();
        gameObject.SetActive(false);
    }
}