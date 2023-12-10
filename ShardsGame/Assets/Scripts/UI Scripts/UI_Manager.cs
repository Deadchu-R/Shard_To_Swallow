using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Serialization;


public class UI_Manager : MonoBehaviour
{
    public enum PanelEnum
    {
        Player_UI_Panel,
        Quest_System_Panel,
        Speech_Bubble_Panel,
        Pause_Menu_Panel,
    }

    [Serializable]
    public class PanelEntry
    {
        public PanelEnum PanelEnum;
        public GameObject PanelObject;
    }

    [SerializeField] private PlayerUI playerUIScript;
    [SerializeField] private QuestManager questSystemScript;
    [Tooltip("Add all panels in the same order as the PanelEnum")] [SerializeField]
    
    private List<PanelEntry> panelEntries = new List<PanelEntry>();
    private Dictionary<PanelEnum, GameObject> panels = new Dictionary<PanelEnum, GameObject>();
   
    
    public void SetQuestSequence(string[] questSequence)
    {
        questSystemScript.SetQuestSequence(questSequence);
    }

    private void OnValidate()
    {
        InitializePanelDictionary();
    }
  
    
    public void OpenEnabledPanels(PanelEnum[] EnabledPanels)
    {
        foreach (var panel in EnabledPanels)
        {
            OpenPanel(panel);
        }
    }


    private void InitializePanelDictionary()
    {
        InitializePanelEntries();
        SetPanelsDictionary();
    }

    private void InitializePanelEntries()
    {  
        AdjustPanelEntriesSize();
        AssignPanelEnums();
    }
    /// <summary>
    /// setting PanelEntries to PanelEnum size.
    /// <para> </para>
    /// <para> this to ensure that all enums always appear in the inspector </para>
    /// </summary>
    private void AdjustPanelEntriesSize()
    {
        if (panelEntries.Count > Enum.GetValues(typeof(PanelEnum)).Length)
        {
            panelEntries.RemoveRange(Enum.GetValues(typeof(PanelEnum)).Length, panelEntries.Count - Enum.GetValues(typeof(PanelEnum)).Length);
        }
        else if (panelEntries.Count < Enum.GetValues(typeof(PanelEnum)).Length)
        {
            int difference = Enum.GetValues(typeof(PanelEnum)).Length - panelEntries.Count;
            for (int i = 0; i < difference; i++)
            {
                panelEntries.Add(new PanelEntry());
            }
        }
    }
/// <summary>
/// Assigns the panel enums.
/// <para>Setting panelEntries order as panel Enums.</para>
/// <para> This is done to make sure that the panels are in the same order as the PanelEnum.</para>
/// </summary>
    private void AssignPanelEnums()
    {
        for (int i = 0; i < panelEntries.Count; i++)
        {
            panelEntries[i].PanelEnum = (PanelEnum)Enum.ToObject(typeof(PanelEnum), i);
        }
    }

    private void SetPanelsDictionary()
    {
        panels.Clear();
        foreach (var entry in panelEntries)
        {
            if (entry.PanelObject == null)
            {
                
                continue;
            }
            if (panels.ContainsValue(entry.PanelObject))
            {
                continue;
            }
            panels[entry.PanelEnum] = entry.PanelObject;
        }
    }

    public void ClosePanel(PanelEnum panelID)
    {
        GameObject panel = panels[panelID];
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void OpenPanel(PanelEnum panelID)
    {
        GameObject panel = panels[panelID];
        if (panel != null && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }
    
    public void SetPlayerUIQuestionText(string text)
    {
        Debug.Log(text);
        playerUIScript.SetQuestionText(text);
        OpenPanel(PanelEnum.Player_UI_Panel);
        Debug.Log("set player ui question text");
    }
}