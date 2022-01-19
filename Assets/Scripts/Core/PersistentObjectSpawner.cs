
using UnityEngine;

namespace com.sluggagames.dragon.Core
{
  public class PersistentObjectSpawner : MonoBehaviour
  {
    [SerializeField] GameObject persistentObject;
    static bool hasSpawned = false;

    private void Awake()
    {
      if (hasSpawned) return;
      SpawnPersistentObject();
      // Update to use Resource.Load and instantiate the prefab.

    }

    private void SpawnPersistentObject()
    {
      GameObject persistentObjectSpawned = Instantiate(persistentObject);
      DontDestroyOnLoad(persistentObject);
    }
  }
}
