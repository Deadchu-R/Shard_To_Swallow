using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //kill player
            print("ouchie");
            Destroy(gameObject);
        }
        else
        {
            
        }
    }
}
