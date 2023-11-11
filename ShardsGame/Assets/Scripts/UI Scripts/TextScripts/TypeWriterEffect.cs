using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeWriterEffect : MonoBehaviour
{

    private TextMeshProUGUI _textComponent;
    

    
    //type writer components
    private int _currentVisibleCharacterIndex;
    private Coroutine _typeWriteCoroutine;
    private bool _readyForNewText = true;
    

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interPunctionDelay;
    
    [Header("TypeWriter Settings")]
    [SerializeField] private float charactersPerSecond = 20f;
    [SerializeField] private float interPunctionDelay = 0.5f;
    
    //Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;
    
    [Header("Skip Options")]
    [SerializeField] private bool quickSkip = false;
    [SerializeField] [Min(1)] private int skipSpeedUp = 5;

    //Event Functionality
    private WaitForSeconds _textBoxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;
    
    public static event Action CompletedTextRevealed;
    public static event Action<char> CharacterRevealed; 
    private void Awake()
    {
       SetTextComponent();
        
        _simpleDelay = new WaitForSeconds(1f / charactersPerSecond);
        _interPunctionDelay = new WaitForSeconds(interPunctionDelay);
        
        _skipDelay = new WaitForSeconds(1f / (charactersPerSecond * skipSpeedUp));
        _textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    public void SetTextComponent()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
        Debug.Log("type writer started");
    }

    private void OnEnable()
    {
        //TextMeshProUGUI
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }


    public void PrepareForNewText(Object obj)
    {
        if (!_readyForNewText) return;
        _readyForNewText = false;
        if (_typeWriteCoroutine != null)
            StopCoroutine(_typeWriteCoroutine);
        
     
        _textComponent.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;
        
        _typeWriteCoroutine = StartCoroutine(Typewriter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_textComponent.maxVisibleCharacters != _textComponent.textInfo.characterCount -1)
            {
                Skip();
            }
        }
    }

    private void Skip()
    {
        if (CurrentlySkipping) return;
        
        CurrentlySkipping = true;

        if (!quickSkip)
        {
            StartCoroutine(SkipSpeedUpReset());
            return;
        }

        StopCoroutine(_typeWriteCoroutine);
        _textComponent.maxVisibleCharacters = _textComponent.textInfo.characterCount;
        _readyForNewText = true;
        CompletedTextRevealed?.Invoke();
    }
    private IEnumerator SkipSpeedUpReset()
    {
       yield return new WaitUntil(() => _textComponent.maxVisibleCharacters == _textComponent.textInfo.characterCount -1);
       CurrentlySkipping = false;
    }

    private IEnumerator Typewriter()
    {
        
        
        TMP_TextInfo textInfo = _textComponent.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1;
            if (_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                _textComponent.maxVisibleCharacters++;
                yield return _textBoxFullEventDelay;
                CompletedTextRevealed?.Invoke();
               _readyForNewText = true; 
                yield break;
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;
            
            _textComponent.maxVisibleCharacters++;

            if (!CurrentlySkipping && character == '?' || character == '.' || character == '!' || character == ','
                || character == ':' || character == ';' || character == '-')
            {
                yield return _interPunctionDelay;
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay;
            }
            CharacterRevealed?.Invoke(character);
            _currentVisibleCharacterIndex++;
        }
    }
}
