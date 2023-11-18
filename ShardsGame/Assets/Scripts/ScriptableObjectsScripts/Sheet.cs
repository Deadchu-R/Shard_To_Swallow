using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Sheet", menuName = "Sheet")]
public class Sheet : ScriptableObject
{
    public Page[] pages;
    UnityEvent OnFinishedSheet = new UnityEvent();

    
    /// <summary>
    /// a constructor to create a sheet with a certain number of pages
    /// </summary>
    /// <param name="numberOfPages">the number of pages inside Page[] pages Array</param>
    public Sheet(int numberOfPages)
    {
        pages = new Page[numberOfPages];
    } 
 
    /// <summary>
    /// a method to check if the sheet contains a certain page
    /// </summary>
    /// <param name="page">the certain page</param>
    /// <returns></returns>
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
