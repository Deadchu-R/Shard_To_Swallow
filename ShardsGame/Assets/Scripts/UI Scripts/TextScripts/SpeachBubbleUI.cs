using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class SpeachBubbleUI : MonoBehaviour
{
    [Header("Events")] [SerializeField] private UnityEvent onEndSpeech;
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

    private Page[] _pages;
    private string _questionText;
    private NPC_DATA _npc;

    [Header("for testing only")] [SerializeField]
    private int currentPageIndex = 0;

    [SerializeField] private int lastPageIndex = 0; // Add this line
    [SerializeField] private Page currentPage;
    [SerializeField] private Page lastPage;
    [SerializeField] private Sheet currentSheet;
    [SerializeField] private Sheet lastSheet;

    private bool _isDialogueActive = false;


    /// <summary>
    /// will set the sheet to the current sheet 
    /// </summary>
    /// <param name="sheet">scriptable object made from Sheet.cs</param>
    /// <param name="npcD">is NPC_DATA which is the NPC itself</param>
    /// <param name="pageIndex">the Index of the page it will start at</param> 
    public void SetSheetUI(Sheet sheet, int pageIndex, NPC_DATA npcD = null)
    {
        //int subLastPageIndex;
        var caller = new StackTrace().GetFrame(1).GetMethod().Name;
        // if call didnt come from NextPage or previousPage check
        if (caller != "NextPage" && caller != "PreviousPage")
        {
            lastPageIndex = currentPageIndex;
            SetLastPage();
        }

        if (caller == "PreviousPage") // problem might be in here
        {
            if (currentSheet.pages.Length > 1)
            {
                lastPage = sheet.pages[pageIndex - 1];
            }

            lastPageIndex = pageIndex - 1;
        }


        onStartSpeech.Invoke();
        if (currentSheet != null)
        {
            if (currentSheet != sheet) // Only update lastSheet and lastPageIndex if moving to a new sheet
            {
                lastSheet = currentSheet;
            }

            currentPageIndex = Array.IndexOf(currentSheet.pages, lastPage); // Use the last page index for the old sheet
            ResetSheet();
        }
        else if (npcD != null)
        {
            _npc = npcD;
        }


        currentSheet = sheet;
        _pages = sheet.pages;
        SetTextSequence(pageIndex);
    }

    /// <summary>
    /// will set the text sequence to the current page
    /// </summary>
    /// <param name="pageIndex"> Index to set the currentPage</param>
    private void SetTextSequence(int pageIndex = 0)
    {
        NextButtonInteractable(false);
        if (_isDialogueActive) return;
        _isDialogueActive = true;
        currentPage = _pages[pageIndex];
        SetPage(currentPage);
        if (_pages.Length > 1) nextButton.onClick.AddListener(NextPage);
        else nextButton.onClick.AddListener(ClosePanel);
    }

    /// <summary>
    /// will set the page to the current page and will set the NPC info
    /// </summary>
    /// <param name="pageToSet">the Page to bet set to</param>
    private void SetPage(Page pageToSet)
    {
        typeWriterEffect.StopText();
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
            NPCIcon.sprite = currentPage.NPCInfo.Icon;
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
                NextButtonInteractable(currentPageIndex + 1 <= currentSheet.pages.Length);

                CloseNonDefaultPageActions();
                break;

            case DialoguePage dialoguePage:
                SetDialogueOptions(dialoguePage);
                NextButtonInteractable(false); // At DialoguePage the next will be disabled
                if (currentPageIndex > 0 ||
                    lastSheet !=
                    null) // will check if having a last page to return to, if yes will enable the back button
                {
                    backButton.interactable = true;
                }

                break;
            default:
                Debug.Log("default action");
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

        NPCIcon.sprite = NPCIcon.sprite == NPCIconSprite ? NPCTalkingIconSprite : NPCIconSprite;
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
        SetLastPage();
        SetCurrentPageIndex(char.Parse("+"));

        Page nextPage = _pages[currentPageIndex];
        if (currentSheet.ContainsPage(nextPage)) //check if the page is in the current sheet
        {
            currentPage = nextPage;
            _isLastPage();
            SetPage(currentPage);
        }
        else
        {
            if (!lastSheet.SheetEquals(currentSheet)) // Only update lastSheet if moving to a new sheet
            {
                lastSheet = currentSheet;
            }

            if (lastSheet != null) //check if there is a last sheet to set the Sheet to
            {
                SetSheetUI(lastSheet, currentPageIndex + 1);
            }
        }
    }

    private void SetLastPage()
    {
        lastPage = currentPage; // Update lastPage here
    }

    /// <summary>
    /// checks if it is the last page of the sheet and will change the next button Listener accordingly 
    /// </summary>
    private void _isLastPage()
    {
        if (currentPageIndex + 1 >= _pages.Length)
        {
            nextButton.onClick.RemoveListener(NextPage);
            nextButton.onClick.AddListener(ClosePanel);
        }
    }

    /// <summary>
    ///  will Move To the previous page
    /// </summary>
    /// <summary>
    ///  will Move To the previous page
    /// </summary>
    private void PreviousPage()
    {
        //lastPageIndex = currentPageIndex; // Update lastPageIndex here
        if (currentSheet.ContainsPage(lastPage)) //check if the page is in the current sheet aka Normal Set Page
        {
            SetCurrentPageIndex(char.Parse("-"));
            if (currentPage.PageEquals(lastPage) && lastSheet != null)
            {
                lastPage = lastSheet.pages[lastPageIndex];
            }

            SetPage(lastPage);
            nextButton.onClick.RemoveListener(ClosePanel);
        }
        else if
            (lastSheet != null &&
             lastSheet !=
             currentSheet) //check if there is a last sheet to set the Sheet and page to, aka new sheet page set
        {
            Debug.Log("i am here");
            //SetLastPage();
            lastPage = lastSheet.pages[currentPageIndex];

            if (currentPageIndex > 0)
            {
                backButton.interactable = true;
                backButton.onClick.AddListener(PreviousPage);
            }

            SetCurrentPageIndex(char.Parse("-"));
            SetSheetUI(lastSheet, lastPageIndex, _npc);
        }
    }

    private void SetCurrentPageIndex(char operatorChar)
    {
        switch (operatorChar)
        {
            case '+':
                if (currentPageIndex < _pages.Length - 1) //check if index won't be out of range
                {
                    currentPageIndex++;
                    backButton.onClick.AddListener(PreviousPage);
                }

                break;
            case '-':
                if (currentPageIndex > 0) //check if there is a previous page in this sheet
                {
                    currentPageIndex--;
                    nextButton.onClick.AddListener(NextPage);
                    backButton.onClick.AddListener(PreviousPage);
                }

                if (currentPageIndex == 0) //check if it is the first page in the sheet
                {
                    backButton.interactable = false;
                    currentPageIndex = 0;
                }

                break;
        }
    }


    /// <summary>
    ///  will close the panel and invoke the onEndSpeech event
    /// </summary>
    private void ClosePanel()
    {
        //ResetBubble();
        currentPage.FinishedPage();
        gameObject.SetActive(false);
        onEndSpeech.Invoke();
        if (_npc != null) _npc.SetIsNpcInteractionStarted(false);
    }

    /// <summary>
    ///  will reset the bubble to the default state
    /// </summary>
    public void ResetBubble()
    {
        currentPageIndex = 0;
    }

    private void ResetSheet()
    {
        currentSheet = null;
        nextButton.onClick.RemoveAllListeners();
        _isDialogueActive = false;
    }
}