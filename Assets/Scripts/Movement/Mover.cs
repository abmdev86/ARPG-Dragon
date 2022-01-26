
using com.sluggagames.dragon.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
namespace com.sluggagames.dragon.Movement
{
  public class Mover : MonoBehaviour, IAction, ISaveable
  {
    NavMeshAgent navMeshAgent;
    Animator animator;
    ActionScheduler actionScheduler;
    Health health;
    [SerializeField]
    AudioClip footstepFX;
    AudioSource audioSource;
    [SerializeField]
    float maxSpeed = 4.5f;

    private void Awake()
    {
      navMeshAgent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
      actionScheduler = GetComponent<ActionScheduler>();
      health = GetComponent<Health>();
      audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
      audioSource.volume = .1f;
    }
    void Update()
    {
      navMeshAgent.enabled = !health.IsDead;
      UpdateAnimator();
    }
    public void StartMoveAction(Vector3 destination, float speedFraction)
    {
      actionScheduler.StartAction(this);

      MoveTo(destination, speedFraction);
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
    public void MoveTo(Vector3 destination, float speedFraction)
    {
      navMeshAgent.destination = destination;
      navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
      navMeshAgent.isStopped = false;
    }

    /// <summary>
    /// Animation Event
    /// </summary>
    void FootR()
    {

      audioSource.PlayOneShot(footstepFX);

    }
    void FootL()
    {
      audioSource.PlayOneShot(footstepFX);

    }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            navMeshAgent.enabled = false;
            transform.position = position.ToVector();
            navMeshAgent.enabled = true;
        }
    }
}
