using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Page", menuName = "Page")]
public class Page : ScriptableObject
{
 public NPC_ID NPCInfo;
 public string Text;
 public string QuestionText = null;
 public int levelToMoveTo = -1;
 [SerializeField] private UnityEvent OnFinishedPage = new UnityEvent();
 public void FinishedPage()
 {
  OnFinishedPage.Invoke();
 }
 
}


