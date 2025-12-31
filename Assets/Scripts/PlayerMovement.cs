using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Transform enemy;
    private SpriteRenderer sr;


    private Animator anim;
    private Rigidbody2D rb;



    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        Vector2 move = new Vector2(x, 0f).normalized;

        if (anim != null)
            anim.SetBool("Walk", Mathf.Abs(x) > 0.01f);

        if (rb != null)
            rb.linearVelocity = new Vector2(move.x * moveSpeed, rb.linearVelocity.y);
        else
            transform.position += (Vector3)(move * moveSpeed * Time.deltaTime);

        if (enemy != null)
        {
            sr.flipX = enemy.position.x < transform.position.x;
        }

    }

    public void SetEnemy(Transform enemyTransform)
    {
        enemy = enemyTransform;
    }

}
