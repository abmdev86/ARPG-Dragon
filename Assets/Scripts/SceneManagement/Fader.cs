using System.Collections;
using UnityEngine;

namespace com.sluggagames.dragon.SceneManagement
{
  public class Fader : MonoBehaviour
  {
    CanvasGroup canvasGroup;
    private void Start()
    {
      canvasGroup = GetComponent<CanvasGroup>();

    }
    IEnumerator FadeOutThenIn()
    {
      yield return FadeOut(3f);
      print("fading out...");
      yield return FadeIn(1f);
      print("Fade in");
    }
    public IEnumerator FadeOut(float time)
    {
      while (canvasGroup.alpha < 1)
      {
        canvasGroup.alpha += Time.deltaTime / time;
        yield return null;
      }
    }
    public IEnumerator FadeIn(float time)
    {
      while (canvasGroup.alpha > 0)
      {

        canvasGroup.alpha -= Time.deltaTime / time;
        yield return null;
      }
    }
  }
}
