
using UnityEngine;
using UnityEngine.Playables;

namespace com.sluggagames.dragon
{
  public class CinematicTrigger : MonoBehaviour
  {
    bool alreadyTriggered = false;
    private void OnTriggerEnter(Collider other)
    {

      if (alreadyTriggered == false && other.tag == "Player")
      {
        alreadyTriggered = true;
        GetComponent<PlayableDirector>().Play();

      }
    }
  }
}
