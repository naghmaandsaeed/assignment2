using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Animator anim;
    [SerializeField] private Health myHealth;


    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 2f;

    private bool isAttacking;
    private SpriteRenderer sr;


    void Awake()
    {
        if (anim == null) anim = GetComponent<Animator>();
        if (myHealth == null) myHealth = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (myHealth != null && myHealth.IsDead) return;
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > stopDistance)
        {
            // chase
            Vector2 dir = new Vector2(player.position.x - transform.position.x,0f).normalized;

            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
            anim.SetBool("Walk", dir.x != 0);

        }
        else
        {
            anim.SetBool("Walk", false);
            // in range: attack with cooldown
            if (!isAttacking)
                StartCoroutine(AttackLoop());
        }

        if (player != null && sr != null)
        {
            sr.flipX = player.position.x > transform.position.x;
        }

    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;
        anim.SetTrigger("Punch");
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void SetPlayer(Transform p)
    {
        player = p;
    }

}

