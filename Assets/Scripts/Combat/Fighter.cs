
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
      return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
    }

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
