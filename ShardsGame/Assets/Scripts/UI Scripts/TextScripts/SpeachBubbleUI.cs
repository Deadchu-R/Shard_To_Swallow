using System;
using TMPro;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeachBubbleUI : MonoBehaviour
{
    [Header("Events")] [SerializeField] private UnityEvent onEndSpeech;

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
    private NPC_DATA npc;
    [SerializeField] private Page currentPage;
    [SerializeField] private Sheet currentSheet;
    private Sheet lastSheet;

    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;

    public void SetSheetUI(Sheet sheet, NPC_DATA npcD = null, int pageIndex = 0)
    {
        if (currentSheet == null)
        {
            npc = npcD;
        }
        else
        {
            currentPageIndex = pageIndex;
            lastPage = currentPage;
            lastSheet = currentSheet;
            ResetBubble();
        }
        currentSheet = sheet;
        this.pages = sheet.pages;
        SetTextSequence(pageIndex);
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
            backButton.onClick.AddListener(PreviousPage);
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
            SetSheetUI(lastSheet);
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

    private void PreviousPage()
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
        if (currentSheet.ContainsPage(lastPage))
        {
            SetPage(lastPage);
        }
        else if (lastSheet != null)
        {
            int pageIndex = Array.IndexOf(lastSheet.pages, lastPage);
            if (pageIndex > 0) backButton.interactable = true;
            Debug.Log("page index is:" + pageIndex);
            SetSheetUI(lastSheet,npc, pageIndex);
        }
    }

    private void ClosePanel()
    {
        Debug.Log("close");
        ResetBubble();
        currentPage.FinishedPage();
        gameObject.SetActive(false);
        onEndSpeech.Invoke();
        if (npc != null) npc.SetIsNpcInteractionStarted(false);
    }

    private void ResetBubble()
    {
        typeWriterEffect.StopText();
        currentSheet = null;
        currentPageIndex = 0;
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}