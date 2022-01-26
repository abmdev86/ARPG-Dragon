using UnityEngine;

namespace com.sluggagames.dragon.EnemySystem
{
  public class PatrolPath : MonoBehaviour
  {
    [Range(0, 1)]
    [SerializeField] float pointRadius = 1f;
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      for (int i = 0; i < transform.childCount; i++)
      {
        int j = GetNextIndex(i);
        Gizmos.DrawSphere(GetWaypointPosition(i), pointRadius);

        Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
      }
    }

    public int GetNextIndex(int i)
    {
      if (i + 1 == transform.childCount)
      {
        return 0;
      }
      return i + 1;
    }

    public Vector3 GetWaypointPosition(int i)
    {
      return transform.GetChild(i).position;
    }
  }
}
