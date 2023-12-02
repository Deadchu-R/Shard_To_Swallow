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
    if (!coll.CompareTag("Player") || !Input.GetKey(KeyCode.E) || isInteractionStarted) return;
    SetIsNpcInteractionStarted(true);
    StartNPCAction();

  }
  
  /// <summary>
  ///  will set the state of the npc interaction to true or false according to the state
  /// </summary>
  /// <param name="state"></param>
  public void SetIsNpcInteractionStarted(bool state)
  {
    isInteractionStarted = state;
  }
  
  /// <summary>
  ///  will start the npc action
  /// </summary>
  private void StartNPCAction()
  {
    NPC_UIHandler.StartDialogue(sheet, this);
  }

}