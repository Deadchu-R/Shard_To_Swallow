using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour
{
    public Material[] materials;
    [SerializeField] private GameObject collision;
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
            collision.SetActive(false);
            this.gameObject.tag = "TileRoad";
        }

        if (other.CompareTag("ShapeCollider"))
        {
            rend.sharedMaterial = materials[0];
            collision.SetActive(true);
            this.gameObject.tag = "Tile";
        }
    }
}

