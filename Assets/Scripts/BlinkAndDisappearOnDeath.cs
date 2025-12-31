using System.Collections;
using UnityEngine;

public class BlinkAndDisappearOnDeath : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private float delayAfterDeath = 0.3f;
    [SerializeField] private int blinks = 6;
    [SerializeField] private float blinkInterval = 0.12f;

    private SpriteRenderer[] renderers;
    private bool started;

    void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();

        // Get ALL sprite renderers on this enemy (root + children)
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    void Update()
    {
        if (started) return;

        if (health != null && health.IsDead)
        {
            started = true;
            StartCoroutine(BlinkThenDestroy());
        }
    }

    private IEnumerator BlinkThenDestroy()
    {
        yield return new WaitForSeconds(delayAfterDeath);

        for (int i = 0; i < blinks; i++)
        {
            SetRenderers(false);
            yield return new WaitForSeconds(blinkInterval);
            SetRenderers(true);
            yield return new WaitForSeconds(blinkInterval);
        }

        Destroy(gameObject);
    }

    private void SetRenderers(bool on)
    {
        foreach (var r in renderers)
            r.enabled = on;
    }
}
