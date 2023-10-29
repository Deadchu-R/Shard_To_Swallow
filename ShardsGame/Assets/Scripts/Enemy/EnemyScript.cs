using System;
using System.Collections;
using System.Collections.Generic;
using Autodesk.Fbx;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  [SerializeField] private GameObject enemyPatrolObj;
  [SerializeField] private GameObject ShapeCollider;
  [SerializeField] private Collider enemyCollider;

  private void OnCollisionEnter(Collision other)
  {
    Debug.Log("collision");
    if (other.collider.CompareTag("TileRoad"))
    {
      Debug.Log("enemy with road");
      enemyPatrolObj.SetActive(false);
      ShapeCollider.SetActive(true);
    }
    
  }
}
