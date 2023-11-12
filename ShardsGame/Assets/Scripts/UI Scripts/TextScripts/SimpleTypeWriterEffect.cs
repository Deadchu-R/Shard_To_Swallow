using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SimpleTypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.02f;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
   [SerializeField] UnityEvent OnFinishedText = new UnityEvent();
    private string fullText;
    
    public void SetText(string text)
    {
        fullText = text;
        textMeshProUGUI.text = ""; // Clear the text initially
        StartCoroutine(TypeText());
    }
    public bool CheckFinishedText()
    {
        if (textMeshProUGUI.text == fullText)
        {
            StopText();
            Debug.Log("finishedtext");
            OnFinishedText.Invoke();
            return true;
        }
        return false;
    }
    public void StopText()
    {
        StopCoroutine(TypeText());
    }

    public void SetFinaleText()
    { 
        textMeshProUGUI.text = fullText;   
    }

    private IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            textMeshProUGUI.text += c;
            CheckFinishedText(); 
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}