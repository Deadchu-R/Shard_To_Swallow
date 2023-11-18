using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SimpleTypeWriterEffect : MonoBehaviour
{
    #region Required Components
    [Header("Required Components:")]
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    #endregion
    
    #region TypeWriter Effect Variables
    [Header("TypeWriter Effect Variables:")]
    [SerializeField] private float typingSpeed = 0.02f;
    #endregion
    
    #region Events
    [Header("Events:")]
    [SerializeField] UnityEvent OnFinishedText = new UnityEvent();
    [SerializeField] UnityEvent OnLetterTyped = new UnityEvent();
    [SerializeField] UnityEvent OnWordTyped = new UnityEvent();
    #endregion
    
    private string fullText;
    
    public void SetText(Page page)
    {
        StopText();
        fullText = page.Text;
        textMeshProUGUI.text = ""; 
        StartCoroutine(TypeText());
    }

    private bool CheckFinishedText() // will check if type-writer finished to write the text
    {
        if (textMeshProUGUI.text != fullText) return false;
        StopText();
        OnFinishedText.Invoke();
        return true;
    }
    private void StopText()
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
            if (c == ' ')
            {
                OnWordTyped.Invoke();
            }
            OnLetterTyped.Invoke();
            textMeshProUGUI.text += c;
            CheckFinishedText(); 
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}