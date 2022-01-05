
using com.sluggagames.dragon.Core;
using com.sluggagames.dragon.Movement;
using UnityEngine;

namespace com.sluggagames.dragon.Combat
{
  public class Fighter : MonoBehaviour, IAction
  {
    Transform target;
    Mover mover;
    ActionScheduler actionScheduler;
    Animator animator;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float weaponDamage = 5f;
    float timeSinceLastAttack = 0;

    private void Awake()
    {
      mover = GetComponent<Mover>();
      actionScheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
    }
    private void Update()
    {
      timeSinceLastAttack += Time.deltaTime;
      if (target == null) return;
      if (!GetIsInRange())
      {
        mover.MoveTo(target.position);
      }
      else
      {
        mover.Cancel();
        AttackBehaviour();
      }

    }

    private void AttackBehaviour()
    {
      if (timeSinceLastAttack > timeBetweenAttacks)
      {
        // Calls Hit() Animation Event
        animator.SetTrigger("attack");
        timeSinceLastAttack = 0;
      }
    }

    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.position) < weaponRange;
    }

    public void Attack(CombatTarget combatTarget)
    {
      actionScheduler.StartAction(this);
      target = combatTarget.transform;
    }

    /// <summary>
    ///  Cancels attack by setting the target as null
    /// </summary>
    public void Cancel()
    {
      target = null;
    }

    void Hit()
    {
      Health targetHealth = target.GetComponent<Health>();
      targetHealth.TakeDamage(weaponDamage);
    }
  }
}
