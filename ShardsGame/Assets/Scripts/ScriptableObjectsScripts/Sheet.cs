using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sheet", menuName = "Sheet")]
public class Sheet : ScriptableObject
{
    public Page[] pages;
    UnityEvent OnFinishedSheet = new UnityEvent();
}
