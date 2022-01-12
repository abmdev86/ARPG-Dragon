using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sluggagames.dragon.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player")
      {
        print("portal triggered!");
      }
    }
  }
}
