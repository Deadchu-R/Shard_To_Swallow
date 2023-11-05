using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPC_DATA : MonoBehaviour
{
  [SerializeField] private string[] texts;
  [SerializeField] private UI_Manager uiManager;
  [SerializeField] private string questionString;
  [SerializeField] private GameObject speechBubble;
  [SerializeField] private bool shouldEnableQuestionText = false;
  [SerializeField] private bool shouldMoveToLevel = false;
  [SerializeField] private int levelToMoveTo = 0;
  [SerializeField] private bool showIcon = false;
  [SerializeField] private Sprite NPCIcon;
  [SerializeField] private string NPCName;
  
 



  private void OnTriggerStay(Collider coll)
  {
    if (coll.CompareTag("Player") && Input.GetKey(KeyCode.E))
    {
      StartDialogue();
    }
  }

  private void DebugText()
  {
    foreach ( string text in texts )
    {
      Debug.Log(text);
    }
  }

  private void StartDialogue()
  {
    SetSpeechBubble();
    uiManager.OpenPanel(2);
  }
  private void SetSpeechBubble()
  {
    SpeachBubbleUI speechBubbleScript = speechBubble.GetComponent<SpeachBubbleUI>(); 
    speechBubbleScript.SetLevelMove(levelToMoveTo, shouldMoveToLevel);
    speechBubbleScript.SetNPCInfo(NPCIcon, NPCName,showIcon);
    speechBubbleScript.SetTextSequence(texts, questionString, shouldEnableQuestionText);
  }
}