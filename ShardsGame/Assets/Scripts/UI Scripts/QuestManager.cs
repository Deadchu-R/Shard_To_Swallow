using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI questText2;
    [SerializeField] private FontStyles fontStyle;
    private string[] questSequence;
    private int questIndex = 0;
    private bool isQuestSequenceActive = false;


    public void SetQuestSequence(string[] questSequence)
    {
        ResetQuestSequence();
        this.questSequence = questSequence;
    }

    private void StartQuestSequence()
    {
        SetQuestText();
        questText.gameObject.SetActive(true);
        isQuestSequenceActive = true;
    }

    public void NextQuest()
    {
        if (!isQuestSequenceActive) return;
        questIndex++;
        if (questIndex >= 2)
        {
            SetQuestText();
        }
        StrikeThoughQuestText();
    }

    private void StrikeThoughQuestText()
    {
        questText.gameObject.SetActive(true);
        questText.fontStyle = FontStyles.Strikethrough;
        questText2.gameObject.SetActive(true);
    }
    

    private void SetQuestText()
    {
        questText.text = questSequence[questIndex];
        if (questIndex < questSequence.Length - 1)
        {
            questText2.text = questSequence[questIndex + 1];
        }
    }

    private void ResetQuestSequence()
    {
        questText.fontStyle = fontStyle;
        questText.gameObject.SetActive(false);
        questText2.gameObject.SetActive(false);
        questIndex = 0;
        isQuestSequenceActive = false;
    }
}