
using UnityEngine;
using UnityEngine.Playables;
using com.sluggagames.dragon.Core;
using com.sluggagames.dragon.Control;

namespace com.sluggagames.dragon.Cinemamtics
{
  public class CinematicControlRemover : MonoBehaviour
  {
    GameObject player;
    PlayableDirector playableDirector;
    private void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      playableDirector = GetComponent<PlayableDirector>();
      playableDirector.played += DisableControls;
      playableDirector.stopped += EnableControls;
    }
    void EnableControls(PlayableDirector pd)
    {
      player.GetComponent<PlayerController>().enabled = true;
      print("Controls enabled ");

    }

    void DisableControls(PlayableDirector pd)
    {
      player.GetComponent<ActionScheduler>().CancelCurrentAction();
      player.GetComponent<PlayerController>().enabled = false;
      print("Controls disabled ");

    }
  }
}
