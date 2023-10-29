using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeachBubbleUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private UI_Manager uiManager;
    [SerializeField] private Image NPCIcon;
    [SerializeField] private TextMeshProUGUI NPCName;
    private bool shouldEnableQuestionText = false;
    private string[] texts;
    private string questionText; 
    public int currentPage = 0;
    private bool shouldMoveToLevel = false;
    private int levelNum = 0;

    private bool isDialogueActive = false;

    public void SetTextSequence(string[] texts, string questionText, bool shouldEnableQuestionText)
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
            this.shouldEnableQuestionText = shouldEnableQuestionText;
            this.questionText = questionText;
            this.texts = texts;
            speechText.text = texts[0];
            backButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
            backButton.interactable = false;
            nextButton.onClick.AddListener(NextPage);
        }
    }

    public void SetNPCInfo(Sprite NPCIcon, string NPCName)
    {
        this.NPCIcon.sprite = NPCIcon;
        this.NPCName.text = NPCName;
    }

    public void SetLevelMove(int levelNumber, bool shouldMoveToLevel)
    {
        this.levelNum = levelNumber;
        this.shouldMoveToLevel = shouldMoveToLevel;
    }


    private void NextPage()
    {
        backButton.gameObject.SetActive(true);
        backButton.interactable = true;
        if (currentPage < texts.Length - 1)
        {
            currentPage++;
            backButton.onClick.AddListener(LastPage);
        }

        speechText.text = texts[currentPage];
        CheckLastPage();
    }

    private void CheckLastPage()
    {
        if (currentPage + 1 == texts.Length)
        {
            nextButton.onClick.AddListener(ClosePanel);
            nextButton.onClick.RemoveListener(NextPage);
        }
    }

    private void LastPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            nextButton.onClick.AddListener(NextPage);
        }

        if (currentPage == 0)
        {
            backButton.interactable = false;
        }

        speechText.text = texts[currentPage];
    }

    private void ClosePanel()
    {
        if (shouldEnableQuestionText) uiManager.SetPlayerUIQuestionText(questionText);
        if (shouldMoveToLevel) SceneManager.LoadScene(levelNum);
        gameObject.SetActive(false);
        currentPage = 0;
        speechText.text = texts[currentPage];
        nextButton.onClick.RemoveAllListeners();
        isDialogueActive = false;
    }
}