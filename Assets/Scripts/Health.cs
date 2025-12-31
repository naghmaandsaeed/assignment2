using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;   // visible in Inspector

    public int CurrentHp => currentHp;

    void Awake()
    {
        currentHp = maxHp;
        Debug.Log($"{name} HP = {currentHp}");
    }

    public void TakeDamage(int dmg)
    {
        if (currentHp <= 0) return;

        currentHp -= dmg;
        if (currentHp < 0) currentHp = 0;
        if (CurrentHp == 0)
        {
            Debug.Log($"{name} DIED");

            var anim = GetComponent<Animator>();
            if (anim != null) anim.SetTrigger("Die");

            var ai = GetComponent<EnemyAI>();
            if (ai != null) ai.enabled = false;

            // Optional: disable body collider so it doesn't keep interacting
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }


        Debug.Log($"{name} took {dmg}, HP = {currentHp}");
    }

    public bool IsDead => currentHp <= 0;
}
