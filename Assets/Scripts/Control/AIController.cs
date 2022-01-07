
using com.sluggagames.dragon.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace com.sluggagames.dragon.Control
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    Fighter fighter;


    private void Start()
    {
      fighter = GetComponent<Fighter>();
      player = GameObject.FindGameObjectWithTag("Player");


    }
    private void Update()
    {
      if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
      {

        fighter.Attack(player);
      }
      else
      {
        fighter.Cancel();
      }


    }

    private bool InAttackRangeOfPlayer()
    {
      float v = Vector3.Distance(transform.position, player.transform.position);
      return v < chaseDistance;
    }
  }
}
