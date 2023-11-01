using System;
using System.Collections;
using System.Collections.Generic;
using Autodesk.Fbx;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  [SerializeField] private GameObject enemyPatrolObj;
  [SerializeField] private GameObject ShapeCollider;
  [SerializeField] private GameObject enemyFather;

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log("collision");
    if (other.CompareTag("TileRoad"))
    {
      Debug.Log("enemy with road");
      enemyPatrolObj.SetActive(false);
      ShapeCollider.SetActive(true);
    }
    
  }
}
