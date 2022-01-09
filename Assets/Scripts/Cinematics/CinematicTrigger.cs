
using UnityEngine;
using UnityEngine.Playables;

namespace com.sluggagames.dragon.Cinematics
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
