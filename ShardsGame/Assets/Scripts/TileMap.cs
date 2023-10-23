using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Vector2 MapSize;
    [SerializeField] private GameObject tilePrefab;
    public TileMap(Vector2 mapSize)
    {
        this.MapSize = mapSize;
    }


}
