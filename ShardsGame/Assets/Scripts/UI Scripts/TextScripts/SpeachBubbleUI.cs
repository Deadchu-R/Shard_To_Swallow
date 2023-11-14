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
    private UI_Manager uiManager;

    [SerializeField] private Image NPCIcon;
    [SerializeField] private TextMeshProUGUI NPCName;

    #endregion

    [Header("SpeechBubble Settings")] [SerializeField]
    private float talkingIconSpeed = 0.5f;
    private Sprite NPCIconSprite;
    private Sprite NPCTalkingIconSprite;

    #region Side Scripts

    [Header("Side Scripts")] [SerializeField]
    private SimpleTypeWriterEffect typeWriterEffect;

    #endregion

    private bool shouldEnableQuestionText = false;
    private string[] texts;
    private string questionText;
    public int currentPage = 0;
    private bool shouldMoveToLevel = false;
    private int levelNum = 0;

    private bool isDialogueActive = false;
    private bool isTalkingIconDisplayed = false;

    private void Awake()
    {
        isDialogueActive = false;
    }

    public void SetTextSequence(string[] texts, string questionText, bool shouldEnableQuestionText)
    {
        uiManager.ClosePanel(0);
        uiManager.OpenPanel(2);
        NextButtonInteractable(false);
        if (isDialogueActive) return;
        isDialogueActive = true;
        this.shouldEnableQuestionText = shouldEnableQuestionText;
        this.questionText = questionText;
        this.texts = texts;
        typeWriterEffect.SetText(texts[0]);


        if (texts.Length > 1) nextButton.onClick.AddListener(NextPage);
        else nextButton.onClick.AddListener(ClosePanel);
    }

    public void SetNPCInfo(Sprite NPCIcon, Sprite NPCTalkingIcon, string NPCName, bool showIcon)
    {
        this.NPCName.text = NPCName;
        if (!showIcon) this.NPCIcon.gameObject.SetActive(false);
        else
        {
            NPCIconSprite = NPCIcon;
            NPCTalkingIconSprite = NPCTalkingIcon;
            this.NPCIcon.sprite = NPCIcon;
            
        }
    }

    public void TalkAnimation()
    {
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

    private IEnumerator TalkAnimationCorutine(Sprite NPCIcon, Sprite NPCTalkingIcon)
    {
        if (this.NPCIcon.sprite == NPCIcon)
        {
            this.NPCIcon.sprite = NPCTalkingIcon;
            isTalkingIconDisplayed = true;
            yield return new WaitForSeconds(talkingIconSpeed);
        }
        else
        {
            this.NPCIcon.sprite = NPCIcon;
            isTalkingIconDisplayed = false;
            yield return new WaitForSeconds(talkingIconSpeed);
        }
    }

    public void NextButtonInteractable(bool interactable)
    {
        nextButton.interactable = interactable;
    }

    public void SetLevelMove(int levelNumber, bool shouldMoveToLevel)
    {
        this.levelNum = levelNumber;
        this.shouldMoveToLevel = shouldMoveToLevel;
    }


    private void NextPage()
    {
        NextButtonInteractable(false);

        backButton.interactable = true;
        if (currentPage < texts.Length - 1)
        {
            currentPage++;
            backButton.onClick.AddListener(LastPage);
        }

        typeWriterEffect.SetText(texts[currentPage]);
        CheckLastPage();
    }

    private void CheckLastPage()
    {
        typeWriterEffect.StopText();
        if (currentPage + 1 != texts.Length) return;
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

        typeWriterEffect.SetText(texts[currentPage]);
    }

    private void ClosePanel()
    {
        typeWriterEffect.StopText();
        if (shouldEnableQuestionText) uiManager.SetPlayerUIQuestionText(questionText);
        if (shouldMoveToLevel) SceneManager.LoadScene(levelNum);
        gameObject.SetActive(false);
        currentPage = 0;
        //typeWriterEffect.SetText(texts[currentPage]);
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}