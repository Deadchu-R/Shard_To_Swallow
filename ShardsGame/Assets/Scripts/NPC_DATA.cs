using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_DATA : MonoBehaviour
{
  [SerializeField] private string[] texts;
  [SerializeField] private UI_Manager uiManager;
  //[SerializeField] private SpeachBubbleUI speechBubble;
  [SerializeField] private GameObject speechBubble;
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
    speechBubbleScript.SetTextSequence(texts);
    uiManager.OpenPanel(2);
    isDialogueActive = true;
  }
}
