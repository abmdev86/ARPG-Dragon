
using UnityEngine;

namespace com.sluggagames.dragon.Core
{
  public class TestMenu : MonoBehaviour
  {
    private void Start()
    {
#if UNITY_WEBGL
      gameObject.SetActive(false);
#endif
    }

    public void QuitGame()
    {
      Application.Quit();
    }
  }
}
