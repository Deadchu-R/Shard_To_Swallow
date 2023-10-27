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
        Debug.Log("collison" + other.name);
        if (other.CompareTag("Shard"))
        {
            rend.sharedMaterial = materials[1];
        }

        if (other.CompareTag("EnemyTileChanger"))
        {
            rend.sharedMaterial = materials[0];
            Debug.Log("Enemy Tile Changer");
        }
    }
}
