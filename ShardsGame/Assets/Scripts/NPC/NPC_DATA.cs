using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPC_DATA : MonoBehaviour
{
  [Header("NPC ID Properties")]
  [SerializeField] private string NPCName;
  [SerializeField] private Sprite NPCIcon;
  [SerializeField] private Sprite NPCTalkingIcon;
  [SerializeField] private bool showIcon = false;
  [SerializeField] private string[] texts;
  [Header("NPC Components")]
  [SerializeField] private UI_Manager uiManager;
  [SerializeField] private GameObject speechBubble;
  [Header("NPC Settings")]
  [SerializeField] private bool shouldMoveToLevel = false;
  [SerializeField] private bool shouldEnableQuestionText = false;
  [SerializeField] private int levelToMoveTo = 0;
  [SerializeField] private string questionString;

  
  private void OnTriggerStay(Collider coll)
  {
    if (coll.CompareTag("Player") && Input.GetKey(KeyCode.E))
    {
      StartDialogue();
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
    speechBubbleScript.SetNPCInfo(NPCIcon,NPCTalkingIcon, NPCName,showIcon);
    speechBubbleScript.SetTextSequence(texts, questionString, shouldEnableQuestionText);
  }
}