using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour
{
    public Material[] materials;
    Renderer rend;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            rend.sharedMaterial = materials[1];
        }
    }
}
