
using com.sluggagames.dragon.Core;
using UnityEngine;
using UnityEngine.AI;

namespace com.sluggagames.dragon.Movement
{
  public class Mover : MonoBehaviour, IAction
  {
    NavMeshAgent navMeshAgent;
    Animator animator;
    ActionScheduler actionScheduler;

    private void Awake()
    {
      navMeshAgent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
      actionScheduler = GetComponent<ActionScheduler>();
    }


    void Update()
    {

      UpdateAnimator();
    }
    public void StartMoveAction(Vector3 destination)
    {
      actionScheduler.StartAction(this);

      MoveTo(destination);
    }

    /// <summary>
    /// Converts NavMeshAgent Global velocity to local velocity for the animation speed.
    /// </summary>
    private void UpdateAnimator()
    {
      Vector3 velocity = navMeshAgent.velocity;
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.z;
      animator.SetFloat("forwardSpeed", speed);
    }

    /// <summary>
    /// Stops the NavMeshAgent
    /// </summary>
    public void Cancel()
    {
      navMeshAgent.isStopped = true;
    }

    /// <summary>
    /// Moves the NavMeshAgent to the desired destination
    /// </summary>
    /// <param name="destination"> Vector3 destination for the NavMeshAgent to travel towards</param>
    public void MoveTo(Vector3 destination)
    {
      navMeshAgent.destination = destination;
      navMeshAgent.isStopped = false;
    }

    /// <summary>
    /// Animation Event
    /// </summary>
     void FootR()
    {
      // play sfx here
      // print("STOMP!");
    }
     void FootL()
    {

    }
  }
}
