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
    [SerializeField] private float typingSpeed = 0.05f;
    #endregion
    
    #region Events
    [Header("Events:")]
    [SerializeField] UnityEvent OnFinishedText = new UnityEvent();
    [SerializeField] UnityEvent OnLetterTyped = new UnityEvent();
    [SerializeField] UnityEvent OnWordTyped = new UnityEvent();
    #endregion
    
    private string fullText;
    
    /// <summary>
    ///  will set the text to the textMeshProUGUI component
    /// </summary>
    /// <param name="page">taking info from page.Text</param>
    public void SetText(Page page)
    {
        StopText();
        fullText = page.Text;
        textMeshProUGUI.text = ""; 
        StartCoroutine(TypeText());
    }

    /// <summary>
    ///  will check if finished typing the text then invoke the OnFinishedText event
    /// </summary>
    /// <returns></returns>
    private bool CheckFinishedText() // will check if type-writer finished to write the text
    {
        if (textMeshProUGUI.text != fullText) return false;
        StopText();
        OnFinishedText.Invoke();
        return true;
    }
    
    /// <summary>
    /// will stop the typing Corutine immediately
    /// </summary>
    private void StopText()
    {
        StopCoroutine(TypeText());
    }

    /// <summary>
    /// Skipping the setting Finale Text
    /// </summary>
    public void SetFinaleText()
    { 
        textMeshProUGUI.text = fullText;   
    }

    /// <summary>
    ///  will start the typing Corutine
    /// Invoking the OnLetterTyped event each letter typed
    /// Invoking the OnWordTyped event each word typed
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
           
            OnLetterTyped.Invoke();
            if (c == ' ')
            {
                OnWordTyped.Invoke();
            }
            textMeshProUGUI.text += c;
            CheckFinishedText(); 
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}