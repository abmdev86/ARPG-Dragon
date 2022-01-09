
using UnityEngine;
using UnityEngine.Playables;
using com.sluggagames.dragon.Core;

namespace com.sluggagames.dragon.Cinemamtics
{
  public class CinematicControlRemover : MonoBehaviour
  {
    PlayableDirector playableDirector;
    private void Start()
    {
      playableDirector = GetComponent<PlayableDirector>();
      playableDirector.played += DisableControls;
      playableDirector.stopped += EnableControls;
    }
    void EnableControls(PlayableDirector pd)
    {
      print("Controls enabled ");
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      player.GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    void DisableControls(PlayableDirector pd)
    {
      print("Controls disabled ");

    }
  }
}
