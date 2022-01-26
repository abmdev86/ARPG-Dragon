
using System;
using com.sluggagames.dragon.Combat;
using com.sluggagames.dragon.Core;
using com.sluggagames.dragon.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace com.sluggagames.dragon.EnemySystem
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class AIController : MonoBehaviour
  {
    GameObject player;
    Fighter fighter;
    Health health;
    Mover mover;
    Vector3 gaurdStartPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;

    int currentWaypointIndex = 0;
    [SerializeField] float suspicionTime = 3;
    [Range(1, 20)]
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] float wayPointDwellTime = 3f;
    [Range(0,1)]
    [SerializeField] float patrolSpeedFraction = 0.2f;


    private void Start()
    {
      fighter = GetComponent<Fighter>();
      player = GameObject.FindGameObjectWithTag("Player");
      health = GetComponent<Health>();
      gaurdStartPosition = transform.position;
      mover = GetComponent<Mover>();
    }
    private void Update()
    {
      if (health.IsDead) return;
      if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
      {
        AttackBehaviour();
      }
      else if (timeSinceLastSawPlayer < suspicionTime)
      {
        //sus state
        SusBehaviour();
      }
      else
      {
        PatrolBehaviour();
      }
      UpdateTimers();

    }

    private void UpdateTimers()
    {
      timeSinceLastSawPlayer += Time.deltaTime;
      timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {

      Vector3 nextPosition = gaurdStartPosition;
      if (patrolPath != null)
      {
        if (AtWaypoint())
        {
          timeSinceArrivedAtWaypoint = 0;
          CycleWaypoint();
        }
        nextPosition = GetCurrentWaypoint();

      }
      if (timeSinceArrivedAtWaypoint > wayPointDwellTime)
      {

        mover.StartMoveAction(nextPosition, patrolSpeedFraction);
      }
    }

    private Vector3 GetCurrentWaypoint()
    {
      return patrolPath.GetWaypointPosition(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
      currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
      float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
      return distanceToWaypoint < waypointTolerance;
    }

    private void SusBehaviour()
    {
      GetComponent<ActionScheduler>().CancelCurrentAction();

    }

    private void AttackBehaviour()
    {
      timeSinceLastSawPlayer = 0;

      GetComponent<NavMeshAgent>().speed = 3.75f;
      fighter.Attack(player);
    }

    private bool InAttackRangeOfPlayer()
    {
      float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
      return distanceToPlayer < chaseDistance;
    }
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);


    }
  }
}
