using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private bool didHitThisSwing = false;

    // Call this when enabling the hitbox
    public void ResetSwing()
    {
        didHitThisSwing = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (didHitThisSwing) return;

        var health = other.GetComponentInParent<Health>();
        if (health == null) return;

        didHitThisSwing = true;

        Debug.Log($"Player hit {health.name} for {damage}");
        health.TakeDamage(damage);

        var enemyAnim = health.GetComponent<Animator>();
        if (enemyAnim != null) enemyAnim.SetTrigger("Hurt");
    }
}

