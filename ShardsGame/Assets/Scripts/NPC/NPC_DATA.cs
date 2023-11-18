using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPC_DATA : MonoBehaviour
{
  [Header("NPC ID Properties")] 
  [SerializeField] private Sheet sheet;
  [Header("NPC Components")]
  [SerializeField] private NPC_UI_Handler NPC_UIHandler;
  private bool isInteractionStarted = false;
  
  private void OnTriggerStay(Collider coll)
  {
    if (coll.CompareTag("Player") && Input.GetKey(KeyCode.E) && !isInteractionStarted)
    {
      SetIsNpcInteractionStarted(true);
      StartNPCAction();
    }
  }
  public void SetIsNpcInteractionStarted(bool state)
  {
    isInteractionStarted = state;
  }
  private void StartNPCAction()
  {
    NPC_UIHandler.StartDialogue(sheet, this);
  }

}