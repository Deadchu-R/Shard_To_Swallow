using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   [SerializeField] private GameObject mainMenuPanel;
   [SerializeField] private GameObject creditsPanel;
   [SerializeField] private GameObject optionsPanel;

    public void StartLevel()
    {
        SceneManager.LoadScene(4);
    }
    
    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
