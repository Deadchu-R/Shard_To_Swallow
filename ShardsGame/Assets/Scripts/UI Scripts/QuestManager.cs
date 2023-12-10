using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI questText2;
    [SerializeField] private FontStyles fontStyle;
    private string[] _questSequence;
    private int questIndex;
    private bool _isQuestSequenceActive;


    public void SetQuestSequence(string[] questSequence)
    {
        ResetQuestSequence();
        _questSequence = questSequence;
        foreach (var quest in _questSequence)
        {
            Debug.Log(quest);
        }
        StartQuestSequence();
    }

    private void StartQuestSequence()
    {
        if (gameObject.activeSelf == false) gameObject.SetActive(true);
        SetQuestText();
        questText.gameObject.SetActive(true);
        _isQuestSequenceActive = true;
    }

    public void NextQuest()
    {
        if (!_isQuestSequenceActive) return;
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
        questText.text = _questSequence[questIndex];
        if (questIndex < _questSequence.Length - 1)
        {
            questText2.text = _questSequence[questIndex + 1];
        }
    }

    private void ResetQuestSequence()
    {
        questText.fontStyle = fontStyle;
        questText.gameObject.SetActive(false);
        questText2.gameObject.SetActive(false);
        questIndex = 1;
        _isQuestSequenceActive = false;
    }
}