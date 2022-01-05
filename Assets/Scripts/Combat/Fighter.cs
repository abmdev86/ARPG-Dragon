
using com.sluggagames.dragon.Core;
using com.sluggagames.dragon.Movement;
using UnityEngine;

namespace com.sluggagames.dragon.Combat
{
  public class Fighter : MonoBehaviour, IAction
  {
    Health target;
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
      if (target.IsDead) return;
      if (!GetIsInRange())
      {
        mover.MoveTo(target.transform.position);
      }
      else
      {
        mover.Cancel();
        AttackBehaviour();
      }

    }

    /// <summary>
    /// Makes Player look at target and attack if timeSinceLastAttack is greater than timeBetweenAttacks.
    /// </summary>
    private void AttackBehaviour()
    {
      transform.LookAt(target.transform);
      if (timeSinceLastAttack > timeBetweenAttacks)
      {
        // Calls Hit() Animation Event
        animator.SetTrigger("attack");
        timeSinceLastAttack = 0;
      }
    }

    /// <summary>
    /// Returns True if the target is within weaponRange.
    /// </summary>
    /// <returns> True if the target is within weaponRange</returns>
    private bool GetIsInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
    }

    /// <summary>
    /// Check if target Health component is not null and the target is alive.
    /// </summary>
    /// <param name="combatTarget">The object you want to attack</param>
    /// <returns></returns>
    public bool CanAttack(CombatTarget combatTarget)
    {
      if (combatTarget == null) { return false; }
      Health targetToCheck = combatTarget.GetComponent<Health>();
      return targetToCheck != null && !targetToCheck.IsDead; //return true if target is Health component is not null and the target is alive.
    }

    /// <summary>
    /// Calls actionScheduler to start combat and gets the combatTarget's Health
    /// </summary>
    /// <param name="combatTarget">The enemy to attack</param>
    public void Attack(CombatTarget combatTarget)
    {
      actionScheduler.StartAction(this);
      target = combatTarget.GetComponent<Health>();
    }

    /// <summary>
    ///  Cancels attack by setting the target as null
    /// </summary>
    public void Cancel()
    {
      animator.SetTrigger("stopAttack");
      target = null;
    }

    void Hit()
    {
      target.TakeDamage(weaponDamage);
    }
  }
}
