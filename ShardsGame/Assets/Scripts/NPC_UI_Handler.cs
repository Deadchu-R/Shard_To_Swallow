using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_UI_Handler : MonoBehaviour
{
   [SerializeField] private SpeachBubbleUI speechBubble;
   
   [SerializeField] private UI_Manager uiManager;
   private Sheet sheet;
   private NPC_DATA npcData;

   public void StartDialogue(Sheet sheet, NPC_DATA npc)
   {
      npcData = npc;
      uiManager.ClosePanel(0);
      this.sheet = sheet;
      uiManager.OpenPanel(2);
      SetSpeechBubble();
   }

   private void SetSpeechBubble()
   {
      SpeachBubbleUI speechBubbleScript = speechBubble.GetComponent<SpeachBubbleUI>();
      speechBubbleScript.SetSheetUI(sheet, npcData); 
   }
}
