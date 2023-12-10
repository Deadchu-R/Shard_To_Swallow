using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnumDictionary<T> : MonoBehaviour where T : Enum
{
    //[System.Serializable] 
    [Tooltip("Add all panels in the same order as the PanelEnum")] [SerializeField]
    public List<EnumEntry> Entries;
    private Dictionary<T, GameObject> dictionary = new Dictionary<T, GameObject>();
    public class EnumEntry
    {
        public T EnumValue;
        public GameObject DictionaryObject;
    }

    public EnumDictionary(List<EnumEntry> genericEntries)
    {
        Entries = genericEntries;
    }

    public void Initialize(List<EnumEntry> genericEntries)
    {
    
        Debug.Log("Initialize EnumDictionary");
        InitializeEntries(genericEntries);
        SetPanelsDictionary();
    }
  

    private void InitializeEntries(List<EnumEntry> genericEntries)
    {
        Entries.Clear();
        foreach (T enumValue in Enum.GetValues(typeof(T)))
        {
            Entries.Add(new EnumEntry { EnumValue = enumValue });
        }
        AdjustPanelEntriesSize(genericEntries);
        AssignPanelEnums(genericEntries);
    }

    private void AdjustPanelEntriesSize(List<EnumEntry> genericEntries)
    {
        if (Entries.Count > Enum.GetValues(typeof(T)).Length)
        {
            Entries.RemoveRange(Enum.GetValues(typeof(T)).Length, Entries.Count - Enum.GetValues(typeof(T)).Length);
            Debug.Log("Removed entries from EnumDictionary");
        }
        else if (Entries.Count < Enum.GetValues(typeof(T)).Length)
        {
            int difference = Enum.GetValues(typeof(T)).Length - Entries.Count;
            for (int i = 0; i < difference; i++)
            {
                Entries.Add(new EnumEntry());
            }
            Debug.Log("Added entries to EnumDictionary");
        }
    }

    private void AssignPanelEnums(List<EnumEntry> genericEntries)
    {
        for (int i = 0; i < Entries.Count; i++)
        {
            Entries[i].EnumValue = (T)Enum.ToObject(typeof(T), i);
        }
    }

    private void SetPanelsDictionary()
    {
        dictionary.Clear();
        foreach (var entry in Entries)
        {
            if (entry.DictionaryObject == null)
            {
                Debug.LogError("No panel assigned for " + entry.EnumValue);
                continue;
            }

            if (dictionary.ContainsValue(entry.DictionaryObject))
            {
                Debug.LogError("Duplicate GameObject found for " + entry.EnumValue);
                continue;
            }

            dictionary[entry.EnumValue] = entry.DictionaryObject;
            entry.DictionaryObject.SetActive(false);
        }
    }
}