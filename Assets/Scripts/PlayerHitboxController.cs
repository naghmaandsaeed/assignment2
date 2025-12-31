using UnityEngine;

public class PlayerHitboxController : MonoBehaviour
{
    [SerializeField] private Collider2D punchCollider;

    void Awake()
    {
        if (punchCollider != null)
            punchCollider.enabled = false;   // force OFF at start
    }

    public void EnablePunchHitbox()
    {
        if (punchCollider == null) return;

        var dmg = punchCollider.GetComponent<HitboxDamage>();
        if (dmg != null) dmg.ResetSwing();

        punchCollider.enabled = true;
    }


    public void DisablePunchHitbox()
    {
        if (punchCollider != null) punchCollider.enabled = false;
    }
}
