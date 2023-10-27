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
  [SerializeField] private Sprite NPCIcon;
  [SerializeField] private string NPCName;
  
  private bool isDialogueActive = false;



  private void OnTriggerStay(Collider coll)
  {
    if (coll.CompareTag("Player") && !isDialogueActive && Input.GetKeyDown(KeyCode.H)) StartDialogue();
  }

  private void StartDialogue()
  {
    foreach ( string text in texts )
    {
      Debug.Log(text);
    }
    SpeachBubbleUI speechBubbleScript = speechBubble.GetComponent<SpeachBubbleUI>(); 
    speechBubbleScript.SetLevelMove(levelToMoveTo, shouldMoveToLevel);
    speechBubbleScript.SetNPCInfo(NPCIcon, NPCName);
    speechBubbleScript.SetTextSequence(texts, questionString, shouldEnableQuestionText);
    uiManager.OpenPanel(2);
    isDialogueActive = true;
    
  }
}
