using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class PageEvent : UnityEvent<Page> { }

public class Page : ScriptableObject
{
 public NPC_ID NPCInfo;
 public string Text;
 public string QuestionText;
 [SerializeField] PageEvent onFinishedPage = new PageEvent();
 public void FinishedPage()
 {
  onFinishedPage.AddListener(GameObject.Find("GameManager").GetComponent<GameManager>().PageAction);
  onFinishedPage.Invoke(this);
 }

 public bool PageEquals(Page other)
 {
  Debug.Log("this Text: " + Text + " other Text: " + other.Text);
  return Text == other.Text && QuestionText == other.QuestionText && NPCInfo == other.NPCInfo;
 }

 public virtual void SetPage()
 {
  
 }

}





