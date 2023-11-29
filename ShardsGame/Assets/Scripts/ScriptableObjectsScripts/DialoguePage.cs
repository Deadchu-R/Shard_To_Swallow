using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Page", menuName = "Page/DialoguePage")]
public class DialoguePage : Page
{
    public string[] Options;
    public Page[] Pages;
    public bool ContainsPage(Page page)
    {
        return Pages.Contains(page);
    }
}
