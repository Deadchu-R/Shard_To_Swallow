using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeachBubbleUI : MonoBehaviour
{
    #region Buttons
    [Header("Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    #endregion

    #region Speech Bubble UI Components
    [Header("Speech Bubble UI Components")] [SerializeField]
    private UI_Manager uiManager;
    [SerializeField] private Image NPCIcon;
    [SerializeField] private TextMeshProUGUI NPCName;
    #endregion

    [Header("SpeechBubble Settings")] [SerializeField]
    private Sprite NPCIconSprite;
    private Sprite NPCTalkingIconSprite;

    #region Side Scripts
    [Header("Side Scripts")]
    [SerializeField] private SimpleTypeWriterEffect typeWriterEffect;
    #endregion
    
    private Page[] pages;
    private string questionText;
    private int currentPage = 0;
    
    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;

    
    private void SetTextSequence()
    {
        NextButtonInteractable(false);
        if (isDialogueActive) return;
        isDialogueActive = true;
        
        this.pages = pages;
        typeWriterEffect.SetText(pages[0]);
        if (pages.Length > 1) nextButton.onClick.AddListener(NextPage);
        else nextButton.onClick.AddListener(ClosePanel);
    }

    public void SetSheetUI(Sheet sheet)
    {
        this.pages = sheet.pages;
        SetNPCInfo();
        SetTextSequence();
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
    
    private void NextPage()
    {
        NextButtonInteractable(false);

        backButton.interactable = true;
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            backButton.onClick.AddListener(LastPage);
        }
        SetNPCInfo();
        typeWriterEffect.SetText(pages[currentPage]);
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

        typeWriterEffect.SetText(pages[currentPage]);
    }

    private void ClosePanel()
    {
        typeWriterEffect.StopText();
        
        if (pages[currentPage].QuestionText != null) uiManager.SetPlayerUIQuestionText(pages[currentPage].QuestionText);
        Debug.Log(pages[currentPage].levelToMoveTo);
        if (pages[currentPage].levelToMoveTo != -1 && pages[currentPage].levelToMoveTo != null) GameManager.Instance.MoveToScene(pages[currentPage].levelToMoveTo);
        
        pages[currentPage].FinishedPage();
        gameObject.SetActive(false);
        currentPage = 0;
;
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}