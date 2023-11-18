using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Page", menuName = "Page/DialoguePage")]
public class DialoguePage : Page
{
    public string[] Options;
    public Page[] Pages;
    public override void SetPage()
    {
        base.SetPage();
    }
}
