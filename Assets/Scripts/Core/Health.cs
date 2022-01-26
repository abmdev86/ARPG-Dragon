using UnityEngine;
using RPG.Saving;

namespace com.sluggagames.dragon.Core
{
  public class Health : MonoBehaviour, ISaveable
  {
    [SerializeField] float healthPoints = 100f;
    Animator animator;
    bool isDead = false;
    public bool IsDead
    {
      get
      {
        return isDead;
      }
    }

    private void Awake()
    {
      animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
      healthPoints = Mathf.Max(healthPoints - damage, 0);
      if (healthPoints == 0)
      {
        Die();
      }
      print(healthPoints);
    }

    private void Die()
    {
      if (isDead) return;
      animator.SetTrigger("die");
      isDead = true;
      GetComponent<ActionScheduler>().CancelCurrentAction();
    }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            
            healthPoints = (float)state;
            if(healthPoints == 0)
            {
                Die();
            }

        }
    }
}
