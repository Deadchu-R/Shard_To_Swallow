using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_UI_Handler : MonoBehaviour
{
   [SerializeField] private SpeechBubbleUI speechBubble;
   [SerializeField] private UI_Manager uiManager;
   private Sheet sheet;
   private NPC_DATA npcData;

   
      /// <summary>
      ///  will start the dialogue with the npc
      /// </summary>
      /// <param name="sheet">Sheet object full of pages (scriptableObject)</param>
      /// <param name="npc">the npc itself (NPC_Data Script of his)</param>
   public void StartDialogue(Sheet sheet, NPC_DATA npc)
   {
      npcData = npc;
      uiManager.ClosePanel(0);
      this.sheet = sheet;
      uiManager.OpenPanel(UI_Manager.PanelEnum.Speech_Bubble_Panel);
      StartSpeechBubble();
   }

      /// <summary>
      ///  will start the speech bubble
      /// </summary>
   private void StartSpeechBubble()
   {
      SpeechBubbleUI speechBubbleScript = speechBubble.GetComponent<SpeechBubbleUI>();
      speechBubbleScript.SetSheetUI(sheet,0, npcData);
   }
}

