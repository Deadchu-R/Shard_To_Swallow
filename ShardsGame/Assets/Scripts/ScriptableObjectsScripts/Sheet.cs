using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sheet", menuName = "Sheet")]
public class Sheet : ScriptableObject
{
    public Page[] pages;
    UnityEvent OnFinishedSheet = new UnityEvent();

    public Sheet(int numberOfPages)
    {
        pages = new Page[numberOfPages];
    } 
 
   public bool ContainsPage(Page page)
   {
      foreach (var pageInSheet in pages)
      {
         if (pageInSheet.Equals(page))
         {
            return true;
         }
      }

      return false;
   }
   public void SetPages(Page[] newPages)
   {
      pages = newPages;
   }
 
    
}
