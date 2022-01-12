
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace com.sluggagames.dragon.SceneManagement
{
  enum DestinationIdentifier
  {
    A, B, C, D
  }
  public class Portal : MonoBehaviour
  {
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destinationIdentifier;
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        StartCoroutine(Transition());
      }
    }

    IEnumerator Transition()
    {
      if (sceneToLoad < 0)
      {
        yield break;
      }
      DontDestroyOnLoad(gameObject);
      yield return SceneManager.LoadSceneAsync(sceneToLoad);

      Portal otherPortal = GetOtherPortal();
      UpdatePlayer(otherPortal);
      print(otherPortal.spawnPoint.position);
      Destroy(gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
      player.transform.position = otherPortal.spawnPoint.position;
      player.transform.rotation = otherPortal.spawnPoint.rotation;

    }

    private Portal GetOtherPortal()
    {
      foreach (Portal portal in FindObjectsOfType<Portal>())
      {
        if (portal == this) continue;
        if (portal.destinationIdentifier != this.destinationIdentifier) continue;
        return portal;
      }
      return null; // no portal found!
    }
  }
}
