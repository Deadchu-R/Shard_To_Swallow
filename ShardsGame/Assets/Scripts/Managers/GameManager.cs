using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] float TimerSeconds = 60f;
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
        SceneManager.LoadScene(1);
    }


}