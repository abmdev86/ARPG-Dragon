
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace com.sluggagames.dragon.Cinematics
{
  public class CinematicTrigger : MonoBehaviour, ISaveable
  {
    bool alreadyTriggered = false;

    public object CaptureState()
    {
      return alreadyTriggered;
    }

    public void RestoreState(object state)
    {
      alreadyTriggered = (bool)state;
    }

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
