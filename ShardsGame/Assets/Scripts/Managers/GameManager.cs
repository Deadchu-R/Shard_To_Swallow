using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] float TimerSeconds = 60f;
    [SerializeField] UI_Manager _uiManager;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        CreateSingleTonInstance();
    }

    private void CreateSingleTonInstance()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        
    }

    public void GhostClickToHub()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    ///  checking for action to do according to the page (npc interaction)
    /// </summary>
    /// <param name="page"></param>
    public void PageAction(Page page)
    {
        switch (page)
        {
            case PageToMoveLevel levelPage:
                MoveToScene(levelPage.levelToMoveTo);
                break;

            default:
                break;
        }

        if (!string.IsNullOrEmpty(page.QuestionText)) _uiManager.SetPlayerUIQuestionText(page.QuestionText);
        
    }



/// <summary>
///  will move to the scene according to the scene index
/// </summary>
/// <param name="sceneIndex"></param>
private void MoveToScene(int sceneIndex)
{
    if (sceneIndex < 0) return;
    SceneManager.LoadScene(sceneIndex);
}
}