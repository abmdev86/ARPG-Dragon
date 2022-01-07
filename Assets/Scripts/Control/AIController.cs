
using com.sluggagames.dragon.Combat;
using com.sluggagames.dragon.Core;
using UnityEngine;
using UnityEngine.AI;

namespace com.sluggagames.dragon.Control
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class AIController : MonoBehaviour
  {
    [Range(1,20)]
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    Fighter fighter;
    Health health;

    private void Start()
    {
      fighter = GetComponent<Fighter>();
      player = GameObject.FindGameObjectWithTag("Player");
      health = GetComponent<Health>();
    }
    private void Update()
    {
      if (health.IsDead) return;
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
      float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
      return distanceToPlayer < chaseDistance;
    }
    private void OnDrawGizmos() {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);


    }
  }
}
