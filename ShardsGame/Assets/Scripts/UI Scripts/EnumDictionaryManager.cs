using System;
using System.Collections.Generic;
using UnityEngine;

public class EnumDictionaryManager<T> : MonoBehaviour where T : Enum
{
    [System.Serializable]
    public class EnumEntry
    {
        public T EnumValue;
        public GameObject EnumObject;
    }

    [System.Serializable]
    public class EnumDictionaryEntry
    {
        public T Key;
        public GameObject Value;
    }

    [SerializeField]
    private List<EnumDictionaryEntry> enumDictionaryEntries = new List<EnumDictionaryEntry>();
    [SerializeField]
    private List<EnumEntry> enumEntries = new List<EnumEntry>();
    private Dictionary<T, GameObject> enumDictionary = new Dictionary<T, GameObject>();

    private void Awake()
    {
        InitializeEnumEntries();
        SetEnumDictionary();
    }

    private void OnValidate()
    {
        InitializeEnumEntries();
        SetEnumDictionary();
    }

    private void InitializeEnumEntries()
    {
        // Your existing code...
    }

    private void SetEnumDictionary()
    {
        enumDictionary.Clear();
        enumDictionaryEntries.Clear();
        foreach (var entry in enumEntries)
        {
            if (entry.EnumObject == null)
            {
                continue;
            }
            if (enumDictionary.ContainsValue(entry.EnumObject))
            {
                continue;
            }
            enumDictionary[entry.EnumValue] = entry.EnumObject;
            entry.EnumObject.SetActive(false);

            enumDictionaryEntries.Add(new EnumDictionaryEntry { Key = entry.EnumValue, Value = entry.EnumObject });
        }
    }

    public void CloseEnumObject(T enumID)
    {
        // Your existing code...
    }

    public void OpenEnumObject(T enumID)
    {
        // Your existing code...
    }
}