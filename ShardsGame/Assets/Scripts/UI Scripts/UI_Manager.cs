using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UI_Manager : MonoBehaviour
{
   
 [SerializeField] private GameObject[] Panels;
   
    public void ClosePanel(int PanelID)
    {
        Panels[PanelID].SetActive(false);
    }
  
    public void OpenPanel(int PanelID)
    {
        Panels[PanelID].SetActive(true);
    }
}
