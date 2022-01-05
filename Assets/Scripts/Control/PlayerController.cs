
using UnityEngine;
using com.sluggagames.dragon.Movement;
using com.sluggagames.dragon.Combat;

namespace com.sluggagames.dragon.Control
{
  public class PlayerController : MonoBehaviour
  {
    Mover mover;
    Fighter fighter;

    private void Awake()
    {
      mover = GetComponent<Mover>();
      fighter = GetComponent<Fighter>();
    }

    private void Update()
    {
      if (InteractWithCombat()) return;
      if (InteractWithMovement()) return;
      print("Nothing to do");
    }

    /// <summary>
    /// Allows player to attack target
    /// </summary>
    private bool InteractWithCombat()
    {
      RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
      foreach (RaycastHit hit in hits)
      {
        CombatTarget target = hit.transform.GetComponent<CombatTarget>();
        if (target == null) continue;
        if (Input.GetMouseButtonDown(0))
        {
          fighter.Attack(target);
        }
        return true;
      }
      return false;
    }

    /// <summary>
    ///  Moves Player to the desired location.
    /// </summary>
    private bool InteractWithMovement()
    {
      RaycastHit hit;
      bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
      if (hasHit)
      {
        if (Input.GetMouseButton(0))
        {

          mover.StartMoveAction(hit.point);
        }
        return true;
      }
      return false;
    }

    /// <summary>
    /// Shoots a ray from the main Camera screen to the mouseposition.
    /// </summary>
    /// <returns>a Ray from the Camera Main screen </returns>
    private static Ray GetMouseRay()
    {
      return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
  }
}
