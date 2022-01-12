
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


namespace com.sluggagames.dragon.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        StartCoroutine(Transition());
      }
    }

    IEnumerator Transition()
    {
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
        return portal;
      }
      return null; // no portal found!
    }
  }
}
