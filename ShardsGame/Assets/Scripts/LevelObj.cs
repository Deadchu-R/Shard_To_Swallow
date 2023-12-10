using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]

[CreateAssetMenu(fileName = "New LevelObj", menuName = "Level/LevelObj")]
public class LevelObj : ScriptableObject
{
  [SerializeField] public Scene scene;
  [SerializeField] private string[] questSequence;
  [SerializeField] public UnityEvent onLevelStart = new UnityEvent();
  private void Awake()
  {
    
  }
}
