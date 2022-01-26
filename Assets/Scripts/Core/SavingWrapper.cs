using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace com.sluggagames.dragon
{
  public class SavingWrapper : MonoBehaviour
  {
    SavingSystem saveSystem;
    const string defaultSaveFile = "save";

    private void Start()
    {
      saveSystem = GetComponent<SavingSystem>();
    }
    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.S))
      {
        Save();
      }
      if (Input.GetKeyDown(KeyCode.L))
      {
        Load();
      }
    }

    public void Load()
    {
      saveSystem.Load(defaultSaveFile);
    }
    public void Save()
    {
      saveSystem.Save(defaultSaveFile);
    }
  }
}
