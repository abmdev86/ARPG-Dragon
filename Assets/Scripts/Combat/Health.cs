using UnityEngine;

namespace com.sluggagames.dragon.Combat
{
  public class Health : MonoBehaviour
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
    }
  }
}
