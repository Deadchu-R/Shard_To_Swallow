using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeachBubbleUI : MonoBehaviour
{
   [SerializeField] private Button backButton;
   [SerializeField] private Button nextButton;
   [SerializeField] private TextMeshProUGUI speechText;
   private string[] texts;
   private int currentPage = 0;
   
   public void SetTextSequence(string[] texts)
   {
      this.texts = texts;
      speechText.text = texts[0];
      backButton.gameObject.SetActive(false);
      nextButton.gameObject.SetActive(true);
      backButton.interactable = false;
      nextButton.onClick.AddListener(NextPage);
      
   }

   private void NextPage()
   {
        backButton.interactable = true;
      if (currentPage < texts.Length - 1)
      {
         currentPage++;
         backButton.onClick.AddListener(LastPage);
      }

      speechText.text = texts[currentPage];
      
   }
 
   private void LastPage()
   {
      if (currentPage > 0)
      {
         currentPage--;
         nextButton.onClick.AddListener(NextPage);
      }
      if (currentPage == 0)
      {
         backButton.interactable = false;
      }
      speechText.text = texts[currentPage];
   }


}
