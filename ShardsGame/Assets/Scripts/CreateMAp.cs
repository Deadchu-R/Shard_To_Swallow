using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class CreateMAp : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Texture2D[] images;
    [SerializeField] private Material[] materials;
    private Vector2 mapSize;
    private Texture2D image;
    List<GameObject> parents = new List<GameObject>();


    
    

    private void Start()
    {
       
        //CreateMap();
    }

    [ContextMenu("CreateMap")]
    private void CreateMap()
    {
       GameObject map = new GameObject("Map")
       {
           transform =
           {
               parent = this.transform
           }
       };
        
        image = images[UnityEngine.Random.Range(0, images.Length)];
        Color[] pixels = image.GetPixels();
        mapSize.x = image.width;
        mapSize.y = image.height;
        float colorComparisonThreshold = 0.1f;

        Vector3[] spawnPositions = new Vector3[pixels.Length];
        Vector3 startSpawnPosition = new Vector3(-Mathf.Round(mapSize.x / 2), 0, -Mathf.Round(mapSize.y / 2));
        Vector3 currentSpawnPosition = startSpawnPosition;

        int counter = 0;
        for (int z = 0; z < mapSize.y; z++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                spawnPositions[counter] = currentSpawnPosition;
                counter++;
                currentSpawnPosition.x++;
            }

            currentSpawnPosition.x = startSpawnPosition.x;
            currentSpawnPosition.z++;
        }
        counter = 0;
        foreach (Vector3 pos in spawnPositions)
        {
            GameObject currentTile = null;
            Color c = pixels[counter];

            if (counter < pixels.Length)
            {
             ColorCheckerGPT(c,currentTile,pos,map,colorComparisonThreshold);
            }
            counter++;
        }
        
    }

    private void CreateTile(string name,GameObject currentTile,Vector3 pos, GameObject map , int matIndex = 0)
    {
        tile.gameObject.GetComponent<Renderer>().material = materials[matIndex];
        currentTile = Instantiate(tile, pos, Quaternion.identity);
        currentTile.name = name;
        currentTile.transform.parent = map.transform;
    }
    private void CreateTile(string name,GameObject currentTile,Vector3 pos, GameObject map , Material mat, GameObject parent = null)
    {
        tile.gameObject.GetComponent<Renderer>().material = mat;
        currentTile = Instantiate(tile, pos, Quaternion.identity);
        currentTile.name = name;
        currentTile.transform.parent = map.transform;
    }

    private void ColorChecker(GameObject currentTile, Vector3 pos, GameObject map, Color c)
    {
        if (c.Equals(Color.red))
        {
            CreateTile("red",currentTile,pos, map, materials[0]);
        }
        if (c.Equals(Color.green))
        {
            CreateTile("green", currentTile,pos, map, materials[1]);
        }
            
        if (c.Equals(Color.blue))
        {
            CreateTile("blue", currentTile,pos, map, materials[2]);
        }
        Debug.Log("obj color is:" + c );
    }
    private void ColorCheckerGPT(Color c , GameObject currentTile, Vector3 pos, GameObject map, float colorComparisonThreshold)
    {
        if (ColorApproximatelyEqual(c, Color.black, colorComparisonThreshold))
        {
            CreateTile("black", currentTile, pos, map, materials[0]);
        }
        else if (ColorApproximatelyEqual(c, Color.white, colorComparisonThreshold))
        {
            CreateTile("white", currentTile, pos, map, materials[0]);
        }
        else if (ColorApproximatelyEqual(c, Color.gray, colorComparisonThreshold))
        {
            CreateTile("gray", currentTile, pos, map, materials[2]);
        }
        else if (ColorApproximatelyEqual(c, Color.magenta, colorComparisonThreshold))
        {
            CreateTile("magenta", currentTile, pos, map, materials[0]);
        }
        else if (ColorApproximatelyEqual(c, Color.blue, colorComparisonThreshold))
        {
            CreateTile("blue", currentTile, pos, map, materials[1]);
        }
    }


    private bool ColorApproximatelyEqual(Color a, Color b, float threshold)
    {
        return Mathf.Abs(a.r - b.r) < threshold &&
               Mathf.Abs(a.g - b.g) < threshold &&
               Mathf.Abs(a.b - b.b) < threshold &&
               Mathf.Abs(a.a - b.a) < threshold;
    }
}