using TMPro;
using System.Collections;
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
    private int currentPage = 0;

    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;


    private void SetTextSequence()
    {
        Debug.Log(pages[currentPage].Text);
        NextButtonInteractable(false);
        if (isDialogueActive) return;
        isDialogueActive = true;
        SetPage();
        if (pages.Length > 1) nextButton.onClick.AddListener(NextPage);
        else nextButton.onClick.AddListener(ClosePanel);
    }

    private void SetDialogueOptions()
    {
        if (pages[currentPage] is DialoguePage && pages[currentPage] != null)
        {
            dialogueOptionsScript.SetDialougeOptions(pages[currentPage] as DialoguePage);
        }
    }

    public void SetSheetUI(Sheet sheet)
    {
        this.pages = sheet.pages;
        SetNPCInfo();
        SetTextSequence();
    }
    public void SetSheetUI(Page[] pages, int currentPage)
    {
        ResetBubble();
        this.pages = pages;
        this.currentPage = currentPage;
        SetTextSequence();
    }


    private void SetPage()
    {
        SetNPCInfo();
        SetDialogueOptions();
        typeWriterEffect.SetText(pages[currentPage]);
    }

    private void SetNPCInfo()
    {
       
        this.NPCName.text = pages[currentPage].NPCInfo.Name;
        if (!pages[currentPage].NPCInfo.ShowIcon) this.NPCIcon.gameObject.SetActive(false);
        else
        {
            NPCIconSprite = pages[currentPage].NPCInfo.Icon;
            NPCTalkingIconSprite = pages[currentPage].NPCInfo.TalkingIcon;
            this.NPCIcon.sprite = pages[currentPage].NPCInfo.Icon;
        }
    }

    public void TalkAnimation()
    {
        if (!pages[currentPage].NPCInfo.ShowIcon) return;

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

    public void NextPage()
    {
        NextButtonInteractable(false);

        backButton.interactable = true;
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            backButton.onClick.AddListener(LastPage);
        }

        SetPage();
        CheckLastPage();
    }

    private void CheckLastPage()
    {
        typeWriterEffect.StopText();
        if (currentPage + 1 != pages.Length) return;
        nextButton.onClick.RemoveListener(NextPage);
        nextButton.onClick.AddListener(ClosePanel);
    }

    private void LastPage()
    {
        typeWriterEffect.StopText();
        if (currentPage > 0)
        {
            currentPage--;
            nextButton.onClick.AddListener(NextPage);
        }

        if (currentPage == 0)
        {
            backButton.interactable = false;
        }

        SetPage();
    }

    private void ClosePanel()
    {
       
        // typeWriterEffect.StopText();
        // currentPage = 0;
        // nextButton.onClick.RemoveAllListeners();
        // isDialogueActive = false;
        ResetBubble();
        
        pages[currentPage].FinishedPage();
        gameObject.SetActive(false);
    }
    private void ResetBubble()
    {
        typeWriterEffect.StopText();
        currentPage = 0;
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}