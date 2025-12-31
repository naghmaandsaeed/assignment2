using System.Collections;
using UnityEngine;

public class EnemyCombatState : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Health health;
    [SerializeField] private EnemyAI ai;
    [SerializeField] private float hurtTime = 0.25f;

    public bool IsHurt { get; private set; }
    public bool IsDead => health != null && health.IsDead;

    void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (health == null) health = GetComponent<Health>();
        if (ai == null) ai = GetComponent<EnemyAI>();
    }

    public void OnDamaged()
    {
        if (IsDead) return;

        StopAllCoroutines();
        StartCoroutine(HurtRoutine());
    }

    private IEnumerator HurtRoutine()
    {
        IsHurt = true;
        anim.SetTrigger("Hurt");
        yield return new WaitForSeconds(hurtTime);
        IsHurt = false;
    }

    public void OnDeath()
    {
        anim.SetTrigger("Die");
        if (ai != null) ai.enabled = false;
    }
}
