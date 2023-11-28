using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class SimpleTypeWriterEffect : MonoBehaviour
{
    #region Required Components

    [Header("Required Components:")] [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    #endregion

    #region TypeWriter Effect Variables

    [Header("TypeWriter Effect Variables:")] [SerializeField]
    private float typingSpeed = 0.05f;

    #endregion

    #region Events

    [Header("Events:")] [SerializeField] UnityEvent onFinishedText = new UnityEvent();
    [SerializeField] UnityEvent onLetterTyped = new UnityEvent();
    [SerializeField] UnityEvent onWordTyped = new UnityEvent();

    #endregion

    private bool _isCoroutineRunning = false;
    private string _fullText;

    /// <summary>
    ///  will set the text to the textMeshProUGUI component
    /// </summary>
    /// <param name="page">taking info from page.Text</param>
    public void SetText(Page page)
    {
        textMeshProUGUI.text = "";
        _fullText = page.Text;
        if (!_isCoroutineRunning) StartCoroutine(TypeText()); // in order to prevent multiple coroutines running at the same time, will check if any coroutine is running
    }

    /// <summary>
    ///  will check if finished typing the text then invoke the OnFinishedText event
    /// </summary>
    /// <returns></returns>
    private bool IsFinishedText() // will check if type-writer finished to write the text
    {
        if (textMeshProUGUI.text == _fullText)
        {
            //Debug.Log("finished text");
            _isCoroutineRunning = false;
            onFinishedText.Invoke();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// will stop the typing Coroutine immediately
    /// </summary>
    public void StopText()
    {
        _isCoroutineRunning = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// Skipping the setting Finale Text
    /// </summary>
    public void SetFinaleText()
    {
        textMeshProUGUI.text = _fullText;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    ///  will start the typing Coroutine
    /// Invoking the OnLetterTyped event each letter typed
    /// Invoking the OnWordTyped event each word typed
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText()
    {
        _isCoroutineRunning = true;
        Debug.Log("TypeText started");
        foreach (char c in _fullText)
        {
            onLetterTyped.Invoke();
            if (c == ' ')
            {
                onWordTyped.Invoke();
            }

            textMeshProUGUI.text += c;
            if (IsFinishedText()) yield break;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}