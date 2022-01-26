
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Saving;

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
    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeWaitTime = 0.5f;

    SavingWrapper savingWrapper;


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
        Debug.LogError("Scene To load is not set", this);
        yield break;
      }
      DontDestroyOnLoad(gameObject);
      Fader fader = FindObjectOfType<Fader>();

      yield return fader.FadeOut(fadeOutTime);

      //save current level
      savingWrapper = FindObjectOfType<SavingWrapper>();
      savingWrapper.Save();


      yield return SceneManager.LoadSceneAsync(sceneToLoad);

      // load level data
      savingWrapper.Load();

      Portal otherPortal = GetOtherPortal();
      UpdatePlayer(otherPortal);
      Destroy(gameObject);

      yield return new WaitForSeconds(fadeWaitTime);
      yield return fader.FadeIn(fadeInTime);




    }
    /// <summary>
    /// Sets the player gameobject to the spawnPoint location
    /// </summary>
    /// <param name="otherPortal">The portal that the player is transitioning to</param>
    private void UpdatePlayer(Portal otherPortal)
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
     // player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
      player.GetComponent<NavMeshAgent>().enabled = false;
      player.transform.position = otherPortal.spawnPoint.position;
      player.transform.rotation = otherPortal.spawnPoint.rotation;
      player.GetComponent<NavMeshAgent>().enabled = true;

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
