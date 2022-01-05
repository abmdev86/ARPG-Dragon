
using UnityEngine;

namespace com.sluggagames.dragon.Core
{
  public class FollowCamera : MonoBehaviour
  {
    [SerializeField] Transform target;

    private void LateUpdate()
    {
      transform.position = target.position;
    }

  }
}
