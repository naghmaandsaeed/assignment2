using UnityEngine;

public class HitboxDamageTrigger : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        var health = other.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage);
    }
}

