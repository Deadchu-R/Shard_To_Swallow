using System;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour
{

    [SerializeField] private UnityEvent onLevelStart;
    [SerializeField] private UI_Manager uiManager;
    [SerializeField] private UI_Manager.PanelEnum[] enabledPanels;
    [SerializeField] private string[] levelQuestSequence;
    [SerializeField] private UnityEvent[] onQuestEnd;
    [SerializeField] private LevelObj levelData;


    private void OnValidate()
    {
        
    }

    private void Awake()
    {
        onLevelStart.Invoke();

        if (levelQuestSequence.Length > 0)
        {
            Debug.Log("LevelManager: Setting quest sequence");
            uiManager.SetQuestSequence(levelQuestSequence);
        }
    
    }
}
