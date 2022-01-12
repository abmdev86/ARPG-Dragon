
using UnityEngine;
using UnityEngine.SceneManagement;


namespace com.sluggagames.dragon.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] int sceneToLoad = -1;
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        SceneManager.LoadScene(sceneToLoad);
      }
    }
  }
}
