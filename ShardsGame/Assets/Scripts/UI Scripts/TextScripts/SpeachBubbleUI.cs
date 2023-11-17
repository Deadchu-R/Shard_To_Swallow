using System;
using TMPro;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeachBubbleUI : MonoBehaviour
{
    #region Buttons

    [Header("Buttons")] [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    #endregion

    #region Speech Bubble UI Components

    [Header("Speech Bubble UI Components")] [SerializeField]
    private Image NPCIcon;

    [SerializeField] private TextMeshProUGUI NPCName;

    #endregion

    [Header("SpeechBubble Settings")] [SerializeField]
    private Sprite NPCIconSprite;

    private Sprite NPCTalkingIconSprite;

    #region Side Scripts

    [Header("Side Scripts")] [SerializeField]
    private SimpleTypeWriterEffect typeWriterEffect;

    [SerializeField] private DialougeOptions dialogueOptionsScript;

    #endregion

    private Page[] pages;
    private string questionText;
    private int currentPageIndex = 0;
    private Page lastPage; 
    [SerializeField] private Page currentPage;
   [SerializeField] private Sheet currentSheet;
    private Sheet lastSheet;

    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;

    public void SetSheetUI(Sheet sheet, int pageIndex = 0)
    {
        currentSheet = sheet;
        this.pages = sheet.pages;
        SetTextSequence( pageIndex);
    }

    public void SetNewSheetUI(Sheet sheet, int pageIndex = 0)
    {
        currentPageIndex = pageIndex;
        Debug.Log("did set the new sheet");
         lastPage = currentPage;
         lastSheet = currentSheet;
        //currentPage = sheet.pages[pageIndex];
      //  currentSheet = sheet;
        ResetBubble();
        SetSheetUI(sheet, pageIndex);
    }

    private void SetTextSequence(int pageIndex = 0)
    {
        NextButtonInteractable(false);
        if (isDialogueActive) return;
        isDialogueActive = true;
        currentPage = pages[pageIndex];
        SetPage(currentPage);
        if (pages.Length > 1) nextButton.onClick.AddListener(NextPage);
        else nextButton.onClick.AddListener(ClosePanel);
    }

    private void SetPage(Page pageToSet)
    {
        currentPage = pageToSet;
        SetNPCInfo();
        PageActions(currentPage);
        typeWriterEffect.SetText(currentPage);
    }

    private void SetNPCInfo()
    {
        this.NPCName.text = currentPage.NPCInfo.Name;
        if (!currentPage.NPCInfo.ShowIcon) this.NPCIcon.gameObject.SetActive(false);
        else
        {
            NPCIconSprite = currentPage.NPCInfo.Icon;
            NPCTalkingIconSprite = currentPage.NPCInfo.TalkingIcon;
            this.NPCIcon.sprite = currentPage.NPCInfo.Icon;
        }
    }

    private void SetDialogueOptions(DialoguePage page)
    {
        dialogueOptionsScript.SetDialougeOptions(page);
    }

    private void PageActions(Page page)
    {
        switch (page)
        {
            case DefaultPage:
                CloseNonDefaultPageActions();
                break;
            case DialoguePage dialoguePage:
                SetDialogueOptions(dialoguePage);
                break;
            default:
                break;
        }
    }

    private void CloseNonDefaultPageActions()
    {
        dialogueOptionsScript.SetActive(false);
    }


    public void TalkAnimation()
    {
        if (!currentPage.NPCInfo.ShowIcon) return;

        if (this.NPCIcon.sprite == NPCIconSprite)
        {
            this.NPCIcon.sprite = NPCTalkingIconSprite;
            isTalkingIconDisplayed = true;
        }
        else
        {
            this.NPCIcon.sprite = NPCIconSprite;
            isTalkingIconDisplayed = false;
        }
    }

    public void NextButtonInteractable(bool interactable)
    {
        nextButton.interactable = interactable;
    }

    private void NextPage()
    {
        typeWriterEffect.StopText();
        NextButtonInteractable(false);

        backButton.interactable = true;
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            backButton.onClick.AddListener(LastPage);
        }

        lastPage = currentPage;
        currentPage = pages[currentPageIndex];
        if (currentSheet.ContainsPage(currentPage))
        {
            isLastPage();
            SetPage(currentPage);
        }

        else if (lastSheet != null)
        {
            SetNewSheetUI(lastSheet);
        }
    }

    private void isLastPage()
    {
        typeWriterEffect.StopText();
        if (currentPageIndex + 1 >= pages.Length)
        {
            nextButton.onClick.RemoveListener(NextPage);
            nextButton.onClick.AddListener(ClosePanel);
        }
    }

    private void LastPage()
    {
        nextButton.onClick.RemoveListener(ClosePanel);
        typeWriterEffect.StopText();
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            nextButton.onClick.AddListener(NextPage);
        }

        if (currentPageIndex == 0)
        {
            backButton.interactable = false;
            currentPageIndex = 0;
        }

        currentPage = pages[currentPageIndex];
        // SetPage(lastPage);
        if (currentSheet.ContainsPage(lastPage))
        {
            SetPage(lastPage);
        }
        else if (lastSheet != null)
        {
            int pageIndex = Array.IndexOf(lastSheet.pages, lastPage);
            if (pageIndex > 0) backButton.interactable = true;
            Debug.Log("page index is:" + pageIndex);
            SetNewSheetUI(lastSheet, pageIndex);
        }
        //lastPage = currentPage;
  
    }

    private void ClosePanel()
    {
        Debug.Log("close");
        ResetBubble();
        currentPage.FinishedPage();
        gameObject.SetActive(false);
    }

    private void ResetBubble()
    {
        typeWriterEffect.StopText();
        currentPageIndex = 0;
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
    
}