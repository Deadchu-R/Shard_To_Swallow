using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer
{
    List<object> dataList = new List<object>();
    public void AddData(object data)
    {
      
            dataList.Add(data);
        
    }
  
}
