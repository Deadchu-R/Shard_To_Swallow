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
        // This is a singleton pattern. It ensures that there is only one instance of this class.
        // If there is already an instance of this class, destroy this one.
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




public void MoveToScene(int sceneIndex)
{
    if (sceneIndex < 0) return;
    SceneManager.LoadScene(sceneIndex);
}
}