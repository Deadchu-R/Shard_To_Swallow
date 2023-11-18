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
    [Header("Events")]
    [SerializeField] private UnityEvent onEndSpeech;
    [SerializeField] private UnityEvent onStartSpeech;
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
    private NPC_DATA npc;

    [Header("for testing only")] [SerializeField]
    private int currentPageIndex = 0;

    [SerializeField] private Page currentPage;
    [SerializeField] private Page lastPage;
    [SerializeField] private Sheet currentSheet;
    [SerializeField] private Sheet lastSheet;

    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;

    /// <summary>
    /// will set the sheet to the current sheet 
    /// </summary>
    /// <param name="sheet">scriptable object made from Sheet.cs</param>
    /// <param name="npcD">is NPC_DATA which is the NPC itself</param>
    /// <param name="pageIndex">the Index of the page it will start at</param> 
    public void SetSheetUI(Sheet sheet, NPC_DATA npcD = null, int pageIndex = 0)
    {
        onStartSpeech.Invoke();
        currentPageIndex = pageIndex;
        if (currentSheet == null)
        {
            npc = npcD;
        }
        else
        {
            lastPage = currentPage;
            lastSheet = currentSheet;
            ResetBubble();
        }

        currentSheet = sheet;
        pages = sheet.pages;
        SetTextSequence(pageIndex);
    }

    /// <summary>
    /// will set the text sequence to the current page
    /// </summary>
    /// <param name="pageIndex"> Index to set the currentPage</param>
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

    /// <summary>
    /// will set the page to the current page and will set the NPC info
    /// </summary>
    /// <param name="pageToSet">the Page to bet set to</param>
    private void SetPage(Page pageToSet)
    {
        currentPageIndex = Array.IndexOf(currentSheet.pages, pageToSet);
        currentPage = pageToSet;
        PageActions(currentPage);
        SetNPCInfo();
        typeWriterEffect.SetText(currentPage);
    }

    /// <summary>
    /// will set the NPC name and icon
    /// </summary>
    private void SetNPCInfo()
    {
        this.NPCName.text = currentPage.NPCInfo.Name;
        if (!currentPage.NPCInfo.ShowIcon) NPCIcon.gameObject.SetActive(false);
        else
        {
            NPCIcon.gameObject.SetActive(true);
            NPCIconSprite = currentPage.NPCInfo.Icon;
            NPCTalkingIconSprite = currentPage.NPCInfo.TalkingIcon;
            this.NPCIcon.sprite = currentPage.NPCInfo.Icon;
        }
    }

    /// <summary>
    /// will set the dialogue options to the buttons
    /// </summary>
    /// <param name="page">Dialogue Page for DialogueOptions SetDialogueOptions</param>
    private void SetDialogueOptions(DialoguePage page)
    {
        dialogueOptionsScript.SetDialougeOptions(page);
    }

    /// <summary>
    /// will run only after TypeWriterEffect is finished typing (through the event)
    /// </summary>
    public void FinishedTyping()
    {
        PageActions(currentPage);
    }

    /// <summary>
    /// Will check if the page has any actions to do and will do them
    /// </summary>
    /// <param name="page"></param>
    private void PageActions(Page page)
    {
        switch (page)
        {
            case DefaultPage:
                Debug.Log(currentSheet.pages.Length);
                if (currentPageIndex + 1 <= currentSheet.pages.Length)
                {
                    NextButtonInteractable(true);
                }
                else
                {
                    NextButtonInteractable(false);
                }
                CloseNonDefaultPageActions();
                break;
            
            case DialoguePage dialoguePage:
                SetDialogueOptions(dialoguePage);
                NextButtonInteractable(false); // At DialoguePage the next will be disabled
                if (currentPageIndex > 0 || lastSheet != null) // will check if having a last page to return to, if yes will enable the back button
                {
                    backButton.interactable = true;
                }

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// will close all the non default page actions
    /// </summary>
    private void CloseNonDefaultPageActions()
    {
        dialogueOptionsScript.SetActive(false);
    }


    /// <summary>
    /// Will change the NPC icon to the talking icon if the NPC is talking
    /// </summary>
    public void TalkAnimation()
    {
        if (!currentPage.NPCInfo.ShowIcon) return;

        if (NPCIcon.sprite == NPCIconSprite)
        {
            NPCIcon.sprite = NPCTalkingIconSprite;
            isTalkingIconDisplayed = true;
        }
        else
        {
            NPCIcon.sprite = NPCIconSprite;
            isTalkingIconDisplayed = false;
        }
    }

    /// <summary>
    /// will set the NextButton interactable according to the bool
    /// </summary>
    /// <param name="interactable">the bool</param>
    public void NextButtonInteractable(bool interactable)
    {
        nextButton.interactable = interactable;
    }

    /// <summary>
    /// will Move To the next page
    /// </summary>
    private void NextPage()
    {
        NextButtonInteractable(false);
        backButton.interactable = true;
        if (currentPageIndex < pages.Length - 1) //check if index wont be out of range
        {
            currentPageIndex++;
            backButton.onClick.AddListener(PreviousPage);
        }

        currentPage = pages[currentPageIndex];
        if (currentSheet.ContainsPage(currentPage)) //check if the page is in the current sheet
        {
            lastPage = currentPage;
            isLastPage();
            SetPage(currentPage);
        }

        else if (lastSheet != null) //check if there is a last sheet to set the Sheet to
        {
            SetSheetUI(lastSheet);
        }
    }

    /// <summary>
    /// checks if it is the last page of the sheet and will change the next button Listener accordingly 
    /// </summary>
    private void isLastPage()
    {
        if (currentPageIndex + 1 >= pages.Length)
        {
            nextButton.onClick.RemoveListener(NextPage);
            nextButton.onClick.AddListener(ClosePanel);
        }
    }

    /// <summary>
    ///  will Move To the previous page
    /// </summary>
    private void PreviousPage()
    {
        int pageIndex = 0;
        if (currentSheet.ContainsPage(lastPage)) //check if the page is in the current sheet aka Normal Set Page
        {
            if (currentPageIndex > 0) //check if there is a previous page in this sheet
            {
                currentPageIndex--;
                nextButton.onClick.AddListener(NextPage);
            }

            if (currentPageIndex == 0) //check if it is the first page in the sheet
            {
                backButton.interactable = false;
                currentPageIndex = 0;
               
            }
            pageIndex = currentPageIndex;
            currentPage = pages[currentPageIndex];
            lastPage = currentSheet.pages[pageIndex];
            SetPage(lastPage);
            nextButton.onClick.RemoveListener(ClosePanel);
        }
        else if
            (lastSheet != null && lastSheet != currentSheet) //check if there is a last sheet to set the Sheet and page to, aka new sheet page set
        {
            currentSheet = lastSheet;
            pageIndex = lastSheet.pages.Length - 1;
            lastPage = currentSheet.pages[pageIndex];
            if (pageIndex > 0) backButton.interactable = true;
            SetSheetUI(lastSheet, npc, pageIndex);
            currentPageIndex = pageIndex;
        }
    }

    /// <summary>
    ///  will close the panel and invoke the onEndSpeech event
    /// </summary>
    private void ClosePanel()
    {
        ResetBubble();
        currentPage.FinishedPage();
        gameObject.SetActive(false);
        onEndSpeech.Invoke();
        if (npc != null) npc.SetIsNpcInteractionStarted(false);
    }

    /// <summary>
    ///  will reset the bubble to the default state
    /// </summary>
    private void ResetBubble()
    {
        currentSheet = null;
        currentPageIndex = 0;
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}