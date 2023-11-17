using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class PageEvent : UnityEvent<Page> { }

public class Page : ScriptableObject
{
 public NPC_ID NPCInfo;
 public string Text;
 public string QuestionText = null;
 [SerializeField] PageEvent OnFinishedPage = new PageEvent();
 public void FinishedPage()
 {
  OnFinishedPage.AddListener(GameObject.Find("GameManager").GetComponent<GameManager>().PageAction);
  OnFinishedPage.Invoke(this);
 }
 
}

[CreateAssetMenu(fileName = "New Page", menuName = "Page/defualtPage")]
public class DefaultPage : Page
{
 
}
[CreateAssetMenu(fileName = "New Page", menuName = "Page/LevelPage")]
public class PageToMoveLevel : Page
{
 public int levelToMoveTo = -1;
}

[CreateAssetMenu(fileName = "New Page", menuName = "Page/DialoguePage")]
public class DialoguePage : Page
{
 public string[] Options;
 public Page[] Pages;
 
}


